using ByteStore.Application.Services;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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
            tokenService.GenerateToken(Utils.GetUserMock().UserId, It.IsAny<string>(), Roles.Seller)).Returns("JWT VALIDO");
        
        var authService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        // Act
        var result = await authService.AuthenticateUser(Utils.GetUserMock());

        // Assert
        Assert.Equal("JWT VALIDO", result);
        tokenServiceMock.Verify(tokenService => 
            tokenService.GenerateToken(Utils.GetUserMock().UserId, Utils.GetUserMock().Username, Roles.Seller), Times.Once);
    }
    
    
    [Fact]
    public async Task AuthenticateUser_WhenUserNotRegistered_ReturnsEmptyString()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserAggregateByUsername(It.IsAny<string>()))
            .ReturnsAsync((UserAggregate)null);

        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var userValidatorMock = new Mock<IUserValidator>();

        var authService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        // Act
        var result = await authService.AuthenticateUser(new User { Username = "nonexistentuser", Password = "password123" });

        // Assert
        Assert.Equal(string.Empty, result);
    }
}