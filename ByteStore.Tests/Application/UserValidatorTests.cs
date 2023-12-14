using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
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
            User = Utils.GetUserMock(),
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
            User = Utils.GetUserMock(),
            Role = Roles.User
        };

        invalidPasswordUser.User.Password = "1234";

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
                User = Utils.GetUserMock()
            });

        var userValidator = new UserValidator(userRepositoryMock.Object);
        var existingUsernameUser = new SignupUserDto
        {
            User = Utils.GetUserMock(),
            Role = Roles.User
        };

        // Act
        var result = await userValidator.ValidateUser(existingUsernameUser);

        // Assert
        Assert.Equal(UserValidatorStatus.UsernameAlreadyExists, result);
    }
}