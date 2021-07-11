using System;

namespace Twitter.DAL.Entities
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime InsertionTime { get; set; }
    }
}
