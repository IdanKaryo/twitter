using System.Net;
using Twitter.Common;

namespace Twitter.User.Models
{
    public class InvalidUserOrPasswordException: HttpStatusCodeException
    {
        public InvalidUserOrPasswordException() : base(HttpStatusCode.BadRequest, "Username or password is incorrect") {}
    }
}
