using Twitter.User.Services;

namespace Twitter.User.Interfaces
{
    public interface IPasswordHasher
    {
        HashedPassword HashPassword(string password, string saltP = null);

        PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string password);
    }
}
