using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Twitter.Authentication.Interfaces;
using Twitter.Authentication.Models;
using Twitter.Models;
using Twitter.Models.Contract;
using Twitter.User.Interfaces;

namespace Twitter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticator _authenticationService;
        private readonly IUserService _userService;
        private readonly IOptions<AppSettings> _appSettings;

        public AuthenticationController(
            IAuthenticator authenticationService, 
            IUserService userService, 
            IOptions<AppSettings> appSettings)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _appSettings = appSettings;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            var response = await _authenticationService.Authenticate(new AuthenticateRequest()
            {
                Username = request.Username,
                Password = request.Password,
                Secret = _appSettings.Value.Secret
            });

            return Ok(response);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            await _userService.AddUser(new User.Models.User()
            {
                Name = request.Username,
                Password = request.Password
            });

            return Ok();
        }
    }
}
