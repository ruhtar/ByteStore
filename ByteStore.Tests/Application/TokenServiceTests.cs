using System.Security.Cryptography;
using System.Text;
using ByteStore.Application.Services;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.ValueObjects;
using Xunit;

namespace ByteStore.Tests.Application
{
    public class TokenServiceTests
    {
        private static readonly string StringKey = "this is my custom Secret key for authentication";
        private static readonly byte[] Key = Encoding.ASCII.GetBytes(StringKey);

        [Fact]
        public void GenerateToken_ReturnsNonEmptyToken()
        {
            // Arrange
            var tokenService = new TokenService();

            // Act
            var token = GenerateValidToken(tokenService);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void ValidateToken_ReturnsTrue_ForValidToken()
        {
            // Arrange
            var tokenService = new TokenService();
            var validToken = GenerateValidToken(tokenService);

            // Act
            var result = tokenService.ValidateToken(validToken, Key);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateToken_ReturnsFalse_ForInvalidToken()
        {
            // Arrange
            var tokenService = new TokenService();
            var invalidToken = "invalid.token.string";

            // Act
            var result = tokenService.ValidateToken(invalidToken, Key);

            // Assert
            Assert.False(result);
        }

        private static string GenerateValidToken(ITokenService tokenService)
        {
            Environment.SetEnvironmentVariable("JWT_SECRET", StringKey);
            var userId = 1;
            var username = "john.doe";
            var role = Roles.User;
            return tokenService.GenerateToken(userId, username, role);
        }
    }
}