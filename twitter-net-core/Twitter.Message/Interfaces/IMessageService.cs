using System.Collections.Generic;
using System.Threading.Tasks;

namespace Twitter.Message.Interfaces
{
    public interface IMessageService
    {
        Task AddMessage(int userId, string message);

        Task<IEnumerable<Models.Message>> GetMessages(string partialUsernameOrNothing);
    }
}
