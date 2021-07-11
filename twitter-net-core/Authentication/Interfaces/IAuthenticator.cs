using System.Threading.Tasks;
using Twitter.Authentication.Models;

namespace Twitter.Authentication.Interfaces
{
    public interface IAuthenticator
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);

        Task<User.Models.User> GetUserByToken(string token, string secret);
    }
}
