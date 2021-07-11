using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter.DAL.Entities;
using Twitter.DAL.Interfaces;
using Twitter.Message.Interfaces;
using Twitter.User.Interfaces;

namespace Twitter.Message.Services
{
    public class MessageService: IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;

        public MessageService(IMessageRepository messageRepository, IUserService userService)
        {
            _messageRepository = messageRepository;
            _userService = userService;
        }

        public async Task AddMessage(int userId, string message)
        {
            await _messageRepository.AddMessage(new MessageEntity()
            {
                UserId = userId,
                Message = message,
                InsertionTime = DateTime.Now
            });
        }

        public async Task<IEnumerable<Models.Message>> GetMessages(string partialUsernameOrNothing)
        {
            var users = (await _userService.GetUsersByFilter(partialUsernameOrNothing)).ToList();

            var messages = await _messageRepository.GetMessages(users.Select(user =>  user.Id));

            return messages.Select(message => TransformToMessageModel(users, message));
        }

        private static Models.Message TransformToMessageModel(IEnumerable<User.Models.User> users, MessageEntity message)
        {
            return new Models.Message()
            {
                User = users.FirstOrDefault(user => user.Id.Equals(message.UserId)),
                Text = message.Message,
                InsertionTime = message.InsertionTime
            };
        }
    }
}
