using System;

namespace Twitter.Message.Models
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime InsertionTime { get; set; }
        public User.Models.User User { get; set; }
    }
}
