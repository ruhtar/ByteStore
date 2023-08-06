using AuthenticationService.Domain.ValueObjects;

namespace AuthenticationService.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, Roles token);
    }
}