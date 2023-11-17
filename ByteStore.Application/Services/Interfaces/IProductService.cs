using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;

namespace ByteStore.Application.Services.Interfaces;

public interface IProductService
{
    Task<Product> AddProduct(ProductDto product);
    Task<bool> DeleteProduct(int id);
    Task<List<Product>> GetAllProducts(GetProductsInputPagination getProductsInputPagination);
    Task<Product> GetProductById(int id);
    Task<bool> UpdateProduct(int id, UpdateProductDto productDto);
    Task<Review> CreateReview(ReviewDto reviewDto);
    Task<List<ReviewDto>> GetReviews(int productId);
}