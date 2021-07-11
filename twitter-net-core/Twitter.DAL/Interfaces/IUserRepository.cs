using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.DAL.Entities;

namespace Twitter.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(UserEntity user);

        Task<UserEntity> GetUsersByName(string username);

        Task<UserEntity> GetUserById(int id);

        Task<List<UserEntity>> GetUsersByFilter(string partialUsernameOrNothing);
    }
}
