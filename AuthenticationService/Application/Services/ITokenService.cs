using AuthenticationService.Domain.Enums;

namespace AuthenticationService.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, Roles token);
    }
}