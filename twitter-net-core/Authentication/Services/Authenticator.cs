using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Twitter.Authentication.Interfaces;
using Twitter.Authentication.Models;
using Twitter.User.Interfaces;

namespace Twitter.Authentication.Services
{
    public class Authenticator: IAuthenticator
    {
        private readonly IUserService _userService;

        public Authenticator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await _userService.GetByCredentials(request.Username, request.Password);

            if (user == null) return null;

            var token = GenerateJwtToken(user, request.Secret);

            return new AuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(User.Models.User user, string secret, int timeoutInDays = 7)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(timeoutInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User.Models.User> GetUserByToken(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
            
            var user = await _userService.GetUserById(userId);

            return user;
        }        
    }
}
