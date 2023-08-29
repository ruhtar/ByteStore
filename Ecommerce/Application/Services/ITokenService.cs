using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username, Roles token);
    }
}