using System.ComponentModel.DataAnnotations;

namespace Twitter.Models.Contract
{
    public class SignInRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}
