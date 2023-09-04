using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string username, Roles token);
    }
}