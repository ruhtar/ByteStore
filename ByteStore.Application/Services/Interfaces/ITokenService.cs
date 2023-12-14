using ByteStore.Domain.ValueObjects;

namespace ByteStore.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(int userId, string username, Roles role);
    bool ValidateToken(string token, byte[] key);
}