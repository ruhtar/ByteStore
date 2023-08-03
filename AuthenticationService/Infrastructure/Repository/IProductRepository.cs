using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Infrastructure.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(Product product);
    }
}