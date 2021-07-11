using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Twitter.Authentication.Interfaces;
using Twitter.Models;
using Twitter.User.Interfaces;

namespace Twitter.Authentication
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public RequestMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IAuthenticator authenticator)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, userService, authenticator, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IUserService userService, IAuthenticator authenticator, string token)
        {
            try
            {
                var user = await authenticator.GetUserByToken(token, _appSettings.Secret);
                
                context.Items[Constants.UserIdPropertyName] = user.Id;
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
