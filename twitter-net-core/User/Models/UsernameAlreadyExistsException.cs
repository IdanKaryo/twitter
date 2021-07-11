using System.Net;
using Twitter.Common;

namespace Twitter.User.Models
{
    public class UsernameAlreadyExistsException : HttpStatusCodeException
    {
        public UsernameAlreadyExistsException() : base(HttpStatusCode.BadRequest, "Username already exists") { }
    }
}