using ByteStore.Domain.ValueObjects;

namespace ByteStore.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string username, Roles token);
        bool ValidateToken(string token);
    }
}