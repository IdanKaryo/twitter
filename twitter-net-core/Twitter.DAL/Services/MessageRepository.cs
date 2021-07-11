using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twitter.DAL.Entities;
using Twitter.DAL.Interfaces;

namespace Twitter.DAL.Services
{
    public class MessageRepository: IMessageRepository
    {
        private readonly string _connectionString;

        public MessageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddMessage(MessageEntity message)
        {
            using (var context = new TwitterDbContext(_connectionString))
            {
                context.Messages.Add(message);

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<MessageEntity>> GetMessages(IEnumerable<int> userIds)
        {
            using (var context = new TwitterDbContext(_connectionString))
            {
                return await context.Messages.Where(message => userIds.Contains(message.UserId)).OrderByDescending(message => message.InsertionTime).ToListAsync();
            }
        }
    }
}
