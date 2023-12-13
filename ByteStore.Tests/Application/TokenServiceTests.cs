using System.Text;
using ByteStore.Application.Services;
using ByteStore.Domain.ValueObjects;
using Xunit;

namespace ByteStore.Tests.Application;

public class TokenServiceTests
{
    private static readonly byte[] key = Encoding.ASCII.GetBytes("ESSAEHUMACHAVESUPERSECRETA");
    
    [Fact]
    public void GenerateToken_ReturnsNonEmptyToken()
    {
        // Arrange
        var tokenGenerator = new TokenService();

        // Act
        var userId = 1;
        var username = "john.doe";
        var role = Roles.User;
        var token = tokenGenerator.GenerateToken(userId, username, role, key);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }
    
    [Fact]
    public void ValidateToken_ReturnsTrue_ForValidToken()
    {
        // Arrange
        var tokenValidator = new TokenService();
        var userId = 1;
        var username = "john.doe";
        var role = Roles.User;
        var validToken = tokenValidator.GenerateToken(userId, username, role, key);

        // Act
        var result = tokenValidator.ValidateToken(validToken, key);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateToken_ReturnsFalse_ForInvalidToken()
    {
        // Arrange
        var tokenValidator = new TokenService();
        var invalidToken = "invalid.token.string";

        // Act
        var result = tokenValidator.ValidateToken(invalidToken, key);

        // Assert
        Assert.False(result);
    }
    
}