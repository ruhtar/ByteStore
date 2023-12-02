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
    public async Task GetAllProducts_ReturnsCorrectProducts()
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
    public async Task AddProduct_ReturnsAddedProduct()
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
        var productRepository = Utils.GetProductRepository();

        var newProduct = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

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
        var productRepository = Utils.GetProductRepository();

        var newProduct = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

        // Act
        var result = await productRepository.GetProductById(42);

        // Assert
        Assert.NotEqual(newProduct.ProductId, 42);
        Assert.Null(result);
    }
    
    [Fact]
    public async Task UpdateProduct_ReturnsTrue()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        var newProduct = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

        // Act
        var updateDto = new UpdateProductDto
        {
            Name = "NOME ATUALIZADO",
            Price = 12121212121,
            ProductQuantity = 11,
            Description = "DESCRICAO ATUALIZADA"
        };
        var result = await productRepository.UpdateProduct(newProduct.ProductId, updateDto);

        var product = await productRepository.GetProductById(newProduct.ProductId);

        // Assert
        Assert.True(result);
        Assert.Equal(product!.Name, updateDto.Name);
        Assert.Equal(product.Price, updateDto.Price);
        Assert.Equal(product.ProductQuantity, updateDto.ProductQuantity);
        Assert.Equal(product.Description, updateDto.Description);
    }
    
    [Fact]
    public async Task UpdateProduct_ReturnsFalse()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        var newProduct = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

        // Act
        var updateDto = new UpdateProductDto();
        var result = await productRepository.UpdateProduct(newProduct.ProductId, updateDto);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsTrue()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        var newProduct = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

        // Act
        var result = await productRepository.DeleteProduct(newProduct.ProductId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteProduct_ReturnsFalse()
    {
        // Arrange
        var productRepository = Utils.GetProductRepository();

        var newProduct = await productRepository.AddProduct(Utils.GetProductMocks()[0]);

        // Act
        var result = await productRepository.DeleteProduct(42);

        // Assert
        Assert.NotEqual(newProduct.ProductId, 42);
        Assert.False(result);
    }
    
    [Fact]
    public async Task CreateReview_ReturnsAddedReview()
    {
        // Arrange
        var context = Utils.GetDbContext();
        var productRepository = new ProductRepository(context);

        var existingProduct = Utils.GetProductMocks()[0];
        context.Products.Add(existingProduct);
        await context.SaveChangesAsync();

        // Act
        var newReview = new Review
        {
            ProductId = existingProduct.ProductId,
            UserId = 23,
            Username = "Admin",
            Rate = 3,
            ReviewText = "TEXTO DA REVIEW"
        };

        var result = await productRepository.CreateReview(newReview);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Review>(result);
        Assert.Equal(newReview.ProductId, result.ProductId);
        Assert.Equal(newReview.UserId, result.UserId);
        Assert.Equal(newReview.Username, result.Username);
        Assert.Equal(newReview.Rate, result.Rate);
        Assert.Equal(newReview.ReviewText, result.ReviewText);
    }
    
    
    [Fact]
    public async Task GetReviews_ReturnsReviewsForProduct()
    {
        // Arrange
        var context = Utils.GetDbContext();
        var productRepository = new ProductRepository(context);
        
        var existingProduct = Utils.GetProductMocks()[0];
        
        var newReview1 = new Review
        {
            ProductId = existingProduct.ProductId,
            UserId = 23,
            Username = "Admin",
            Rate = 3,
            ReviewText = "TEXTO DA REVIEW"
        };
        
        var newReview2 = new Review
        {
            ProductId = existingProduct.ProductId,
            UserId = 4324,
            Username = "JOAO GRILLO",
            Rate = 4,
            ReviewText = "TEXTO DA REVIEW 2"
        };
        
        existingProduct.Reviews = new List<Review> { newReview1, newReview2 };
        context.Products.Add(existingProduct);
        await context.SaveChangesAsync();

        // Act
        var result = await productRepository.GetReviews(existingProduct.ProductId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Review>>(result);
        Assert.Equal(newReview1.Id, result[0].Id);
        Assert.Equal(newReview2.Id, result[1].Id);
    }
}