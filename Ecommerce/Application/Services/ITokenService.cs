using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId, Roles token);
    }
}