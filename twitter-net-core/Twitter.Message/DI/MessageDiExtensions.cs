using Microsoft.Extensions.DependencyInjection;
using Twitter.DAL.Interfaces;
using Twitter.DAL.Services;

namespace Twitter.Message.DI
{
    public static class MessageDiExtensions
    {
        public static void AddMessageDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IMessageRepository>(x => new MessageRepository(connectionString));
        }
    }
}
