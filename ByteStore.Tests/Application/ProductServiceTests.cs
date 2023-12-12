using ByteStore.Application.Services;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
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
        productRepo.Verify(x=>x.GetAllProducts(input), Times.Once);
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
        Assert.Equal(product, result);
        productRepo.Verify(x=>x.GetProductById(product.ProductId), Times.Once);
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
        productRepo.Verify(x=>x.GetProductById(wrongId), Times.Once);
    }

    [Fact]
    public async Task AddProduct_ShouldReturnAddedProduct()
    {
        //arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];
        
        productRepo.Setup(x => x.AddProduct(It.IsAny<Product>())).ReturnsAsync(product);

        var productService = new ProductService(productRepo.Object);
        
        var productDto = new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            ProductQuantity = product.ProductQuantity,
            ImageStorageUrl = product.ImageStorageUrl,
            Description = product.Description
        };
        
        //Act
        var result = await productService.AddProduct(productDto);
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(productDto.Name, result.Name);
        Assert.Equal(productDto.ProductQuantity, result.ProductQuantity);
        Assert.Equal(productDto.Price, result.Price);
        Assert.Equal(productDto.ImageStorageUrl, result.ImageStorageUrl);
        Assert.Equal(productDto.Description, result.Description);

        productRepo.Verify(repo => repo.AddProduct(It.IsAny<Product>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateProduct()
    {
        //arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];

        productRepo.Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<UpdateProductDto>())).ReturnsAsync(true);

        var productService = new ProductService(productRepo.Object);
        
        var productDto = new UpdateProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            ProductQuantity = product.ProductQuantity,
            ImageStorageUrl = product.ImageStorageUrl,
            Description = product.Description
        };
        
        //act
        var result = await productService.UpdateProduct(product.ProductId ,productDto);

        //assert
        Assert.True(result);
        productRepo.Verify(x=>x.UpdateProduct(product.ProductId, productDto), Times.Once);
    }

    [Fact]
    public async Task CreateReview()
    {
         //arrange
         var productRepo = new Mock<IProductRepository>();
         var product = Utils.GetProductsMock()[0];

         var review = new Review
         {
             ProductId = product.ProductId,
             UserId = Utils.GetUserMock().UserId,
             Username = Utils.GetUserMock().Username,
             ReviewText = "lalalalala",
             Rate = 2
         };
         
         var reviewDto = new ReviewDto
         {
             ProductId = product.ProductId,
             UserId = Utils.GetUserMock().UserId,
             Username = Utils.GetUserMock().Username,
             ReviewText = "lalalalala",
             Rate = 2
         };
         
         productRepo.Setup(x => x.CreateReview(It.IsAny<Review>())).ReturnsAsync(review);
         productRepo.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);
         
         var productService = new ProductService(productRepo.Object);

         //act
         var result = await productService.CreateReview(reviewDto);

         //assert
         Assert.NotNull(result);
         Assert.Equal(result, review);
         productRepo.Verify(x=>x.GetProductById(product.ProductId), Times.Once);
         productRepo.Verify(x=>x.CreateReview(It.IsAny<Review>()), Times.Once);
    }
}