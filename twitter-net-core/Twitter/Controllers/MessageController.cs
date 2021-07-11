using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Twitter.Authentication;
using Twitter.Message.Interfaces;
using Twitter.Models;
using Twitter.Models.Contract;

namespace Twitter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<MessagesHub, IHubClient> _hubContext;

        public MessageController(IMessageService messageService, IHubContext<MessagesHub, IHubClient> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpPost("PostMessage")]
        [Authorize]
        public async Task<IActionResult> PostMessage(PostMessageRequest request)
        {
            var isUserIdAvailable = int.TryParse(HttpContext.Items[Constants.UserIdPropertyName]?.ToString(), out var userId);

            if (!isUserIdAvailable)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Missing authenticated user info");
            }

            await _messageService.AddMessage(userId, request.Message);
                
            await _hubContext.Clients.All.BroadcastMessage();

            return Ok();
        }

        [HttpPost("GetMessages")]
        [Authorize]
        public async Task<IActionResult> GetMessages(GetMessagesRequest request)
        {
            var messages = await _messageService.GetMessages(request.PartialUsernameOrNothing);

            return Ok(messages);
        }
    }
}
