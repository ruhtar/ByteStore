using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Shared.DTO;

namespace ByteStore.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProducts(GetProductsInputPagination input)
    {
        return await _productRepository.GetAllProducts(input);
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _productRepository.GetProductById(id);
    }

    public async Task<Product> AddProduct(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            ProductQuantity = productDto.ProductQuantity,
            Price = productDto.Price,
            ImageStorageUrl = productDto.ImageStorageUrl,
            Description = productDto.Description
        };
        return await _productRepository.AddProduct(product);
    }

    public async Task<bool> UpdateProduct(int id, UpdateProductDto productDto)
    {
        return await _productRepository.UpdateProduct(id, productDto);
    }

    public async Task CreateReview(ReviewDto reviewDto)
    {
        var product = await GetProductById(reviewDto.ProductId);
        if (product == null) return; //handle this error

        if (product.TimesRated == 0)
        {
            product.TimesRated++;
            product.Rate = reviewDto.Rate;
        }
        else
        {
            product.TimesRated++;
            product.Rate = ((product.Rate * (product.TimesRated - 1)) + reviewDto.Rate) / product.TimesRated;
        }

        var review = new Review
        {
            ProductId = reviewDto.ProductId,
            UserId = reviewDto.UserId,
            Username = reviewDto.Username,
            Rate = reviewDto.Rate,
            ReviewText = reviewDto.ReviewText
        };
        
        await _productRepository.CreateReview(review);
    }

    public async Task<List<ReviewDto>> GetReviews(int productId)
    {
        return await _productRepository.GetReviews(productId);
    }

    public async Task<bool> DeleteProduct(int id)
    {
        return await _productRepository.DeleteProduct(id);
    }
}