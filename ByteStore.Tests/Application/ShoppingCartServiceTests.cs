using System.Text.Json;
using ByteStore.Application.Services;
using ByteStore.Domain;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using Moq;
using Xunit;

namespace ByteStore.Tests.Application;

public class ShoppingCartServiceTests
{
    [Fact]
    public async Task BuyOrder_SuccessfulOrder()
    {
        // Arrange
        const int userAggregateId = 1;
        var shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var list = new List<OrderItem>
        {
            new() { ProductId = Utils.GetProductsMock()[0].ProductId, Quantity = Utils.GetProductsMock()[0].ProductQuantity },
            new() { ProductId = Utils.GetProductsMock()[1].ProductId, Quantity = Utils.GetProductsMock()[1].ProductQuantity },
        };
        
        var shoppingCart = new ShoppingCart
        {
            ShoppingCartId = 1,
            UserAggregateId = userAggregateId,
            OrderItems = JsonSerializer.SerializeToUtf8Bytes(list) 
        };

        var shoppingCartDto = new ShoppingCartDto
        {
            UserAggregateId = userAggregateId,
            ShoppingCartId = shoppingCart.ShoppingCartId,
            OrderItems = list
        };
        
        shoppingCartRepositoryMock.Setup(repo => repo.GetShoppingCartByUserAggregateId(userAggregateId))
            .ReturnsAsync(shoppingCartDto);

        productRepositoryMock.Setup(repo => repo.GetProductById(1))
            .ReturnsAsync(Utils.GetProductsMock()[0]);

        productRepositoryMock.Setup(repo => repo.GetProductById(2))
            .ReturnsAsync(Utils.GetProductsMock()[1]);

        var service = new ShoppingCartService(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await service.BuyOrder(userAggregateId);

        // Assert
        Assert.Equal(BuyOrderStatus.Completed, result);
        
        shoppingCartRepositoryMock
            .Verify(repo => repo.RemoveProductFromCart(userAggregateId, It.IsAny<int>()), Times.Never);
        
        productRepositoryMock
            .Verify(repo => repo.DeleteProduct(It.IsAny<int>()), Times.Exactly(2));
        
        userRepositoryMock
            .Verify(repo => repo.UpdatePurchaseHistory(userAggregateId, It.IsAny<List<Product>>()), Times.Once);
        
        shoppingCartRepositoryMock
            .Verify(repo => repo.BuyOrder(userAggregateId), Times.Once);
    }
    
    [Fact]
    public async Task BuyOrder_InvalidQuantity()
    {
        // Arrange
        const int userAggregateId = 1;
        var shoppingCartRepositoryMock = new Mock<IShoppingCartRepository>();
        var productRepositoryMock = new Mock<IProductRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var list = new List<OrderItem>
        {
            new() { ProductId = Utils.GetProductsMock()[0].ProductId, Quantity = Utils.GetProductsMock()[0].ProductQuantity },
            new() { ProductId = Utils.GetProductsMock()[1].ProductId, Quantity = Utils.GetProductsMock()[1].ProductQuantity },
        };
        
        var shoppingCart = new ShoppingCart
        {
            ShoppingCartId = 1,
            UserAggregateId = userAggregateId,
            OrderItems = JsonSerializer.SerializeToUtf8Bytes(list) 
        };
        
        var shoppingCartDto = new ShoppingCartDto
        {
            UserAggregateId = userAggregateId,
            ShoppingCartId = shoppingCart.ShoppingCartId,
            OrderItems = list
        };

        var product1 = new Product
        {
            ProductId = 1,
            ProductQuantity = 3,
        };

        shoppingCartRepositoryMock.Setup(repo => repo.GetShoppingCartByUserAggregateId(userAggregateId))
            .ReturnsAsync(shoppingCartDto);

        productRepositoryMock.Setup(repo => repo.GetProductById(1))
            .ReturnsAsync(product1);

        var service = new ShoppingCartService(shoppingCartRepositoryMock.Object, productRepositoryMock.Object, userRepositoryMock.Object);

        // Act
        var result = await service.BuyOrder(userAggregateId);

        // Assert
        Assert.Equal(BuyOrderStatus.InvalidQuantity, result);
        
        productRepositoryMock
            .Verify(repo => repo.UpdateProduct(It.IsAny<int>(), It.IsAny<UpdateProductDto>()), Times.Never);

        userRepositoryMock
            .Verify(repo => repo.UpdatePurchaseHistory(userAggregateId, It.IsAny<List<Product>>()),
            Times.Never);
        
        shoppingCartRepositoryMock
            .Verify(repo => repo.BuyOrder(userAggregateId), Times.Never);
    }
}