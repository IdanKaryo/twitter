using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twitter.DAL.Entities;
using Twitter.DAL.Interfaces;

namespace Twitter.DAL.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> AddUser(UserEntity userEntity)
        {
            using (var context = new TwitterDbContext(_connectionString))
            {
                if (DoesUserAlreadyExist(userEntity, context))
                {
                    return false;
                }
                
                context.Users.Add(userEntity);

                await context.SaveChangesAsync();
            }

            return true;
        }

        private static bool DoesUserAlreadyExist(UserEntity userEntity, TwitterDbContext context)
        {
            return context.Users.Any(user => user.Name.ToLower() == userEntity.Name.ToLower());
        }

        public async Task<List<UserEntity>> GetUsersByFilter(string partialUsernameOrNothing)
        {
            using (var context = new TwitterDbContext(_connectionString))
            {
                return await context.Users.Where(user =>
                    // ReSharper disable once StringIndexOfIsCultureSpecific.1 - cultural comparison is not supported by entities framework
                    user.Name.ToLower().IndexOf(partialUsernameOrNothing.ToLower()) > -1).ToListAsync();
            }
        }

        public async Task<UserEntity> GetUsersByName(string username)
        {
            using (var context = new TwitterDbContext(_connectionString))
            {
                return await context.Users.SingleOrDefaultAsync(user =>
                    user.Name.ToLower() == username.ToLower());
            }
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            using (var context = new TwitterDbContext(_connectionString))
            {
                return await context.Users.SingleOrDefaultAsync(user => user.Id.Equals(id));
            }
        }
    }
}
