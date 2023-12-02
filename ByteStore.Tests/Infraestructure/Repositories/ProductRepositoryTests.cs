using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
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
        await productRepository.AddProduct(Utils.GetProductMocks()[0]); 
        await productRepository.AddProduct(Utils.GetProductMocks()[1]);
        
        var input = new GetProductsInputPagination { PageSize = 2, PageIndex = 0 };
        var result = await productRepository.GetAllProducts(input);
            
        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
        Assert.Equal(input.PageSize, result.Count);
        Assert.Equivalent(Utils.GetProductMocks()[0], result[0]);
        Assert.Equivalent(Utils.GetProductMocks()[1], result[1]);
    }
    
    [Fact]
    internal async Task AddProduct_ReturnsAddedProduct()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        // Act
        var result = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(Utils.GetProductMocks()[0]!.ProductId, result.ProductId);
    }
    
    [Fact]
    public async Task GetProductById_ReturnsCorrectProduct()
    {
        // Arrange
        var dbContext = Utils.GetDbContext();
        var productRepository = new ProductRepository(dbContext);

        // Add a product for testing
        var newProduct = Utils.GetProductMocks()[0];
        await productRepository.AddProduct(newProduct);

        // Act
        var result = await productRepository.GetProductById(newProduct.ProductId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Product>(result);
        Assert.Equal(newProduct.ProductId, result.ProductId);
    }
    
    [Fact]
    public async Task GetProductById_ReturnsNull()
    {
        // Arrange
        var dbContext = Utils.GetDbContext();
        var productRepository = new ProductRepository(dbContext);

        // Add a product for testing
        var newProduct = Utils.GetProductMocks()[0];
        await productRepository.AddProduct(newProduct);

        // Act
        var result = await productRepository.GetProductById(42);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateProduct_ReturnsTrueOnSuccess()
    {
        // Arrange
        var dbContext = Utils.GetDbContext();
        var productRepository = new ProductRepository(dbContext);

        // Add a product for testing
        var newProduct = new Product { /* Set product properties */ };
        await productRepository.AddProduct(newProduct);

        // Act
        var updateDto = new UpdateProductDto { /* Set updated properties */ };
        var result = await productRepository.UpdateProduct(newProduct.ProductId, updateDto);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsTrueOnSuccess()
    {
        // Arrange
        var dbContext = Utils.GetDbContext();
        var productRepository = new ProductRepository(dbContext);

        // Add a product for testing
        var newProduct = new Product { /* Set product properties */ };
        await productRepository.AddProduct(newProduct);

        // Act
        var result = await productRepository.DeleteProduct(newProduct.ProductId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetReviews_ReturnsReviewsForProduct()
    {
        // Arrange
        var dbContext = Utils.GetDbContext();
        var productRepository = new ProductRepository(dbContext);

        // Add a product with reviews for testing
        var newProduct = new Product { /* Set product properties */ };
        var newReview = new Review { /* Set review properties */ };
        newProduct.Reviews = new List<Review> { newReview };
        await productRepository.AddProduct(newProduct);

        // Act
        var result = await productRepository.GetReviews(newProduct.ProductId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Review>>(result);
        Assert.Equal(newReview.Id, result.FirstOrDefault()?.Id);
    }

    [Fact]
    public async Task CreateReview_ReturnsAddedReview()
    {
        // Arrange
        var dbContext = Utils.GetDbContext();
        var productRepository = new ProductRepository(dbContext);

        // Add a product for testing
        var newProduct = new Product { /* Set product properties */ };
        await productRepository.AddProduct(newProduct);

        // Act
        var newReview = new Review { /* Set review properties */ };
        var result = await productRepository.CreateReview(newReview);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Review>(result);
        Assert.Equal(newReview.Id, result.Id);
    }
}