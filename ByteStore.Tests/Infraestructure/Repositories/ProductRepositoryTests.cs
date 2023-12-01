using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Repositories;
using ByteStore.Shared.DTO;
using Xunit;

namespace ByteStore.Tests.Infraestructure.Repositories;
//https://dejanstojanovic.net/aspnet/2019/september/unit-testing-repositories-in-aspnet-core-with-xunit-and-moq/
public class ProductRepositoryTests
{
    [Fact]
    internal async Task GetAllProducts_ReturnsCorrectProducts()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        // Act
        var input = new GetProductsInputPagination { PageSize = 10, PageIndex = 0 };
        var result = await productRepository.GetAllProducts(input);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
        Assert.Equal(input.PageSize, result.Count);
    }
    
    [Fact]
    internal async Task AddProduct_ReturnsAddedProduct()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        // Act
        var result = await productRepository.AddProduct(Utils.GetProductMock());

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(Utils.GetProductMock().ProductId, result.ProductId);
    }
}