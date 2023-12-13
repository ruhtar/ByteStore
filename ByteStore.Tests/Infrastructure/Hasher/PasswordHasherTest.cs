using ByteStore.Infrastructure.Hasher;
using Xunit;

namespace ByteStore.Tests.Infrastructure.Hasher;

public class PasswordHasherTest
{
    [Fact]
    public void Validate_ReturnsTrue_ForCorrectPassword()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        const string password = "MyStrongPassword123";

        // Act
        var hashedPassword = passwordHasher.Hash(password);
        var result = passwordHasher.Validate(hashedPassword, password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_ReturnsFalse_ForIncorrectPassword()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        const string correctPassword = "MyStrongPassword123";
        const string incorrectPassword = "WrongPassword456";

        // Act
        var hashedPassword = passwordHasher.Hash(correctPassword);
        var result = passwordHasher.Validate(hashedPassword, incorrectPassword);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_ReturnsFalse_ForModifiedHash()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        const string password = "MyStrongPassword123";

        // Act
        var hashedPassword = passwordHasher.Hash(password);
        // Modify the hash
        hashedPassword = hashedPassword.Replace('A', 'B');
        var result = passwordHasher.Validate(hashedPassword, password);

        // Assert
        Assert.False(result);
    }
}