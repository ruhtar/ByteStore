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
            .ReturnsAsync(Utils.GetProductsMock());


        var productService = new ProductService(productRepo.Object);
        //Act
        var result = await productService.GetAllProducts(input);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(Utils.GetProductsMock().Count, result.Count);

        for (var i = 0; i < Utils.GetProductsMock().Count; i++)
        {
            Assert.Equal(Utils.GetProductsMock()[i].ProductId, result[i]?.ProductId);
            Assert.Equal(Utils.GetProductsMock()[i].Name, result[i]?.Name);
            Assert.Equal(Utils.GetProductsMock()[i].Price, result[i]?.Price);
        }
    }

    [Fact]
    public async Task GetProductById_ReturnsAProduct()
    {
        //Arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];
        productRepo.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);

        var productService = new ProductService(productRepo.Object);

        //Act
        var result = await productService.GetProductById(product.ProductId);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, product);
    }
    
    [Fact]
    public async Task GetProductById_ReturnsNull()
    {
        //Arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];
        var wrongId = product.ProductId + 1;
        productRepo.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);

        var productService = new ProductService(productRepo.Object);

        //Act
        var result = await productService.GetProductById(wrongId);
        
        //Assert
        Assert.Null(result);
    }
}