using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Twitter.DAL.Interfaces;
using Twitter.DAL.Services;
using Twitter.User.Interfaces;
using Twitter.User.Services;

namespace Twitter.User.DI
{
    public static class UserDiExtensions
    {
        public static void AddUserDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IUserRepository>(x => new UserRepository(connectionString));
            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
