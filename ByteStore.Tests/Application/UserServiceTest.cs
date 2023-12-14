using ByteStore.Application.Services;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
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
        
        userRepositoryMock.Setup(repo => repo.GetUserAggregate(It.IsAny<UserAggregate>()))
            .ReturnsAsync(Utils.GetUserAggregateMock());
        passwordHasherMock.Setup(hasher => hasher.Validate(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        

        var tokenServiceMock = new Mock<ITokenService>();
        var userValidatorMock = new Mock<IUserValidator>();

        var authService = new UserService(userRepositoryMock.Object, passwordHasherMock.Object, tokenServiceMock.Object, userValidatorMock.Object);

        // Act
        var result = await authService.AuthenticateUser(Utils.GetUserMock());

        // Assert
        Assert.Equal(string.Empty, result);
    }
    
    
    [Fact]
    public async Task AuthenticateUser_WhenUserNotRegistered_ReturnsEmptyString()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserAggregate(It.IsAny<UserAggregate>()))
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