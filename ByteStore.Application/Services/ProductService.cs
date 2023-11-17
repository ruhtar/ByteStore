using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Entities;
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
        await _productRepository.CreateReview(reviewDto);
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