using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.DAL.Entities;
using Twitter.DAL.Interfaces;
using Twitter.User.Interfaces;
using Twitter.User.Models;

namespace Twitter.User.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task AddUser(Models.User user)
        {
            var hashedPassword = _passwordHasher.HashPassword(user.Password);

            var userWasAddedSuccessfully = await _userRepository.AddUser(new UserEntity()
            {
                Name = user.Name,
                Password = hashedPassword.Hash,
                Salt = hashedPassword.Salt
            });

            if (!userWasAddedSuccessfully)
            {
                throw new UsernameAlreadyExistsException();
            }
        }

        public async Task<IEnumerable<Models.User>> GetUsersByFilter(string partialUsernameOrNothing)
        {
            var users = await _userRepository.GetUsersByFilter(partialUsernameOrNothing);

            return users.Select(TransformUser);
        }

        public async Task<Models.User> GetByCredentials(string username, string password)
        {
            var user = await _userRepository.GetUsersByName(username);

            if (user == null)
            {
                throw new InvalidUserOrPasswordException();
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                throw new InvalidUserOrPasswordException();
            }

            return TransformUser(user);
        }

        public async Task<Models.User> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            return TransformUser(user);
        }

        private static Models.User TransformUser(UserEntity user)
        {
            return new Models.User()
            {
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };
        }
    }
}
