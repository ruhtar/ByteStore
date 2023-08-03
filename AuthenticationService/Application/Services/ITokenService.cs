namespace AuthenticationService.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, string token);
    }
}