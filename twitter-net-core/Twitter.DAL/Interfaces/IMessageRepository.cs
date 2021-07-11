using System.Collections.Generic;
using System.Threading.Tasks;
using Twitter.DAL.Entities;

namespace Twitter.DAL.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessage(MessageEntity message);

        Task<List<MessageEntity>> GetMessages(IEnumerable<int> userIds);
    }
}
