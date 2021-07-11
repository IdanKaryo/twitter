using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.User.Interfaces
{
    public interface IUserService
    {
        Task AddUser(Models.User user);

        Task<IEnumerable<Models.User>> GetUsersByFilter(string partialUsernameOrNothing);

        Task<Models.User> GetByCredentials(string username, string password);

        Task<Models.User> GetUserById(int id);
    }
}
