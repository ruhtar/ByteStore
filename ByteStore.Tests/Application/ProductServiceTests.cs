using ByteStore.Application.Services;
using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using Moq;
using Xunit;

namespace ByteStore.Tests.Application;

public class ProductServiceTests
{
    [Theory]
    [InlineData(0, 5)]
    [InlineData(1, 5)]
    [InlineData(2, 3)]
    public async Task GetAllProducts_ReturnsAListOfProducts(int pageIndex, int pageSize)
    {
        //Arrange
        var productRepo = new Mock<IProductRepository>();
        
        var input = new GetProductsInputPagination
        {
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        productRepo.Setup(repo => repo.GetAllProducts(input))!
            .ReturnsAsync(Utils.GetProductMocks());


        var productService = new ProductService(productRepo.Object);
        //Act
        var result = await productService.GetAllProducts(input);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(Utils.GetProductMocks().Count, result.Count);

        for (var i = 0; i < Utils.GetProductMocks().Count; i++)
        {
            Assert.Equal(Utils.GetProductMocks()[i].ProductId, result[i]?.ProductId);
            Assert.Equal(Utils.GetProductMocks()[i].Name, result[i]?.Name);
            Assert.Equal(Utils.GetProductMocks()[i].Price, result[i]?.Price);
        }
    }
}