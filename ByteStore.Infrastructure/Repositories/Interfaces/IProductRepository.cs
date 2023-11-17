using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;

namespace ByteStore.Infrastructure.Repository.Interfaces;

public interface IProductRepository
{
    Task<Product> AddProduct(Product product);
    Task<bool> DeleteProduct(int id);
    Task<List<Product>> GetAllProducts(GetProductsInputPagination getProductsInputPagination);
    Task<Product> GetProductById(int id);
    Task<bool> UpdateProduct(int id, UpdateProductDto product);
    Task CreateReview(ReviewDto reviewDto);
    Task<List<ReviewDto>> GetReviews(int productId);
}