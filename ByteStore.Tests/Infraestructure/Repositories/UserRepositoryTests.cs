using ByteStore.Domain.Aggregates;
using ByteStore.Infrastructure.Repositories;
using ByteStore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ByteStore.Tests.Infraestructure.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public async Task RegisterUser_AddsUserToDatabaseAndCreatesShoppingCart()
    {
        // Arrange
        var context = Utils.GetDbContext();
        var shoppingCartRepository = new Mock<IShoppingCartRepository>();
        var userRepository = new UserRepository(context, shoppingCartRepository.Object);

        var newUser = new UserAggregate
        {
            // Set user properties as needed for the test
        };

        // Act
        await userRepository.RegisterUser(newUser);

        // Assert
        var addedUser = await context.UserAggregates.FirstOrDefaultAsync(u => u.UserAggregateId == newUser.UserAggregateId);
        Assert.NotNull(addedUser);
        Assert.Equal(newUser.UserAggregateId, addedUser.UserAggregateId);

        // Verify that CreateShoppingCart method was called
        shoppingCartRepository.Verify(repo => repo.CreateShoppingCart(newUser.UserAggregateId), Times.Once);
    }
}