namespace AuthenticationService.Authentication
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Validate(string passwordHash, string password);
    }
}