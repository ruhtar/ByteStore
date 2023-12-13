using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using Moq;
using Xunit;

namespace ByteStore.Tests.Application;

public class UserValidatorTests
{
    [Fact]
    public async Task ValidateUser_ReturnsSuccess_ForValidUser()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserAggregate(It.IsAny<UserAggregate>()))
            .ReturnsAsync((UserAggregate)null);

        var userValidator = new UserValidator(userRepositoryMock.Object);
        var validUser = new SignupUserDto
        {
            User = new User { Username = "newuser", Password = "!ValidPassword123" },
            Role = Roles.User
        };

        // Act
        var result = await userValidator.ValidateUser(validUser);

        // Assert
        Assert.Equal(UserValidatorStatus.Success, result);
    }

    [Fact]
    public async Task ValidateUser_ReturnsInvalidPassword_ForInvalidPassword()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userValidator = new UserValidator(userRepositoryMock.Object);
        var invalidPasswordUser = new SignupUserDto
        {
            User = new User { Username = "newuser", Password = "invalid" },
            Role = Roles.User
        };

        // Act
        var result = await userValidator.ValidateUser(invalidPasswordUser);

        // Assert
        Assert.Equal(UserValidatorStatus.InvalidPassword, result);
    }
    

    [Fact]
    public async Task ValidateUser_ReturnsUsernameAlreadyExists_ForExistingUsername()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserAggregate(It.IsAny<UserAggregate>()))
            .ReturnsAsync(new UserAggregate()
            {
                User = new User { Username = "existinguser", Password = "!123ValidPassword123" }
            });

        var userValidator = new UserValidator(userRepositoryMock.Object);
        var existingUsernameUser = new SignupUserDto
        {
            User = new User { Username = "existinguser", Password = "!123ValidPassword123" },
            Role = Roles.User
        };

        // Act
        var result = await userValidator.ValidateUser(existingUsernameUser);

        // Assert
        Assert.Equal(UserValidatorStatus.UsernameAlreadyExists, result);
    }
}