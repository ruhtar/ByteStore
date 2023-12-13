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
    public async Task UpdateProduct_ShouldReturnTrue()
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
    public async Task UpdateProduct_ShouldReturnFalse()
    {
        //arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];

        productRepo.Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<UpdateProductDto>())).ReturnsAsync(false);

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
        Assert.False(result);
        productRepo.Verify(x=>x.UpdateProduct(product.ProductId, productDto), Times.Once);
    }

    [Fact]
    public async Task CreateReview_ShouldReturnNotNull()
    {
         //arrange
         var productRepo = new Mock<IProductRepository>();
         var product = Utils.GetProductsMock()[0];

         var review = new Review
         {
             ProductId = product.ProductId,
             UserId = Utils.GetUserMock().UserId,
             Username = Utils.GetUserMock().Username,
             ReviewText = "BALDURSGATE3 É GOTY FÁCIL",
             Rate = 2
         };
         
         var reviewDto = new ReviewDto
         {
             ProductId = product.ProductId,
             UserId = Utils.GetUserMock().UserId,
             Username = Utils.GetUserMock().Username,
             ReviewText = "BALDURSGATE3 É GOTY FÁCIL",
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
    
    [Fact]
    public async Task CreateReview_ShouldReturnNull()
    {
        //arrange
        var productRepo = new Mock<IProductRepository>();
        var product = Utils.GetProductsMock()[0];

        var review = new Review
        {
            ProductId = product.ProductId,
            UserId = Utils.GetUserMock().UserId,
            Username = Utils.GetUserMock().Username,
            ReviewText = "BALDURSGATE3 É GOTY FÁCIL",
            Rate = 5
        };
         
        var reviewDto = new ReviewDto
        {
            ProductId = product.ProductId,
            UserId = Utils.GetUserMock().UserId,
            Username = Utils.GetUserMock().Username,
            ReviewText = "BALDURSGATE3 É GOTY FÁCIL",
            Rate = 5
        };
         
        productRepo.Setup(x => x.CreateReview(It.IsAny<Review>())).ReturnsAsync(review);
        productRepo.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync((Product)null);
         
        var productService = new ProductService(productRepo.Object);

        //act
        var result = await productService.CreateReview(reviewDto);

        //assert
        Assert.Null(result);
        Assert.NotEqual(result, review);
        productRepo.Verify(x=>x.GetProductById(product.ProductId), Times.Once);
        productRepo.Verify(x=>x.CreateReview(It.IsAny<Review>()), Times.Never);
    }

    [Fact]
    public async Task GetReviewsByProductId_ReturnsNull_WhenReviewsNotFound()
    {
        // Arrange
        var productId = 1;
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(repo => repo.GetReviews(productId)).ReturnsAsync((List<Review>)null);
        var productService = new ProductService(mockProductRepository.Object);

        // Act
        var result = await productService.GetReviewsByProductId(productId);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetReviewsByProductId_ReturnsReviewDtoList_WhenReviewsFound()
    {
        // Arrange
        var productId = 1;
        var mockProductRepository = new Mock<IProductRepository>();
        var mockReviewList = new List<Review>
        {
            new Review { UserId = 1, Username = "user1", Rate = 5, ReviewText = "Great product" },
            new Review { UserId = 2, Username = "user2", Rate = 4, ReviewText = "Good product" }
        };
        mockProductRepository.Setup(repo => repo.GetReviews(productId)).ReturnsAsync(mockReviewList);
        var productService = new ProductService(mockProductRepository.Object);

        // Act
        var result = await productService.GetReviewsByProductId(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(mockReviewList.Count, result.Count);
    }
}