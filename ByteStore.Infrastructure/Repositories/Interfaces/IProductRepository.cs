using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;

namespace ByteStore.Infrastructure.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> AddProduct(Product? product);
    Task<bool> DeleteProduct(int id);
    Task<List<Product?>> GetAllProducts(GetProductsInputPagination getProductsInputPagination);
    Task<Product?> GetProductById(int id);
    Task<bool> UpdateProduct(int id, UpdateProductDto product);
    Task<Review> CreateReview(Review reviewDto);
    Task<List<Review>?> GetReviews(int productId);
}