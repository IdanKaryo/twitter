using System.Text.Json.Serialization;

namespace Twitter.User.Models
{
    
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
