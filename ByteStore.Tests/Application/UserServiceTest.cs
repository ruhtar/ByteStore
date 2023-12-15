using ByteStore.Application.Services;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Repositories.Interfaces;
using Moq;
using Xunit;

namespace ByteStore.Tests.Application;

public class UserServiceTest
{
    [Fact]
    public async Task AuthenticateUser_WhenUserRegistered_ReturnsJwt()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var userValidatorMock = new Mock<IUserValidator>();
        
        userRepositoryMock.Setup(repo => repo.GetUserAggregateByUsername(Utils.GetUserAggregateMock().User.Username))
            .ReturnsAsync(Utils.GetUserAggregateMock());
        
        passwordHasherMock.Setup(hasher => hasher.Validate(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        tokenServiceMock.Setup(tokenService =>
            tokenService.GenerateToken(Utils.GetUserMock().UserId, Utils.GetUserMock().Username, Roles.User)).Returns("JWT VALIDO");
        
        var authService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        // Act
        var result = await authService.AuthenticateUser(Utils.GetUserMock());

        // Assert
        Assert.Equal("JWT VALIDO", result);
        
        tokenServiceMock.Verify(tokenService => 
            tokenService.GenerateToken(Utils.GetUserMock().UserId, Utils.GetUserMock().Username, Roles.User), Times.Once);
        
        userRepositoryMock.Verify(repo =>
            repo.GetUserAggregateByUsername(Utils.GetUserAggregateMock().User.Username), Times.Once);

        passwordHasherMock.Verify(hasher =>
            hasher.Validate(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
    
    
    [Fact]
    public async Task AuthenticateUser_WhenUserNotRegistered_ReturnsEmptyString()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserAggregateByUsername(Utils.GetUserMock().Username))
            .ReturnsAsync((UserAggregate)null);

        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var userValidatorMock = new Mock<IUserValidator>();

        var authService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        // Act
        var result = await authService.AuthenticateUser(new User { Username = "nonexistentuser", Password = "password123" });

        // Assert
        Assert.Equal(string.Empty, result);
        
        tokenServiceMock.Verify(tokenService => 
            tokenService.GenerateToken(Utils.GetUserMock().UserId, Utils.GetUserMock().Username, Roles.User), Times.Never);
        
        userRepositoryMock.Verify(repo =>
            repo.GetUserAggregateByUsername("nonexistentuser"), Times.Once);

        passwordHasherMock.Verify(hasher =>
            hasher.Validate(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
    
    [Fact]
    public async Task AuthenticateUser_WhenInvalidPassword_ReturnsEmptyToken()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var userValidatorMock = new Mock<IUserValidator>();

        // Mock user retrieval by returning a user with a known username
        userRepositoryMock.Setup(repo => repo.GetUserAggregateByUsername(Utils.GetUserMock().Username))
            .ReturnsAsync(Utils.GetUserAggregateMock());

        // Mock password validation to always return false
        passwordHasherMock.Setup(hasher => hasher.Validate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        var authService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        // Act
        var result = await authService.AuthenticateUser(new User { Username = Utils.GetUserMock().Username, Password = "invalidPassword" });

        // Assert
        Assert.Equal(string.Empty, result);

        userRepositoryMock.Verify(repo =>
            repo.GetUserAggregateByUsername(Utils.GetUserMock().Username), Times.Once);

        passwordHasherMock.Verify(hasher =>
            hasher.Validate(It.IsAny<string>(), "invalidPassword"), Times.Once);

        tokenServiceMock.Verify(tokenService =>
            tokenService.GenerateToken(It.IsAny<int>(), It.IsAny<string>(), Roles.User), Times.Never);
    }
    
    [Fact]
    public async Task EditUserAddress_ValidAddress_EditsUserAddress()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var userValidatorMock = new Mock<IUserValidator>();
        
        var userService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        const int userId = 1;

        // Act
        await userService.EditUserAddress(Utils.GetAddressMock(), userId);

        // Assert
        userRepositoryMock.Verify(repo =>
            repo.EditUserAddress(It.IsAny<Address>(), userId), Times.Once);
    }

}