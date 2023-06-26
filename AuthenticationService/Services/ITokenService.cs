namespace AuthenticationService.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}