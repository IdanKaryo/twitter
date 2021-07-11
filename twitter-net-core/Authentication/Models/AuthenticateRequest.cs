using System.ComponentModel.DataAnnotations;

namespace Twitter.Authentication.Models
{
    public class AuthenticateRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Secret { get; set; }
    }
}
