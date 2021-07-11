using System.Threading.Tasks;

namespace Twitter.Models
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
