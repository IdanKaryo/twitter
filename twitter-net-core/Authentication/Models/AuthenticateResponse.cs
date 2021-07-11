namespace Twitter.Authentication.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User.Models.User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Token = token;
        }
    }
}
