using AuthenticationService.Entities;

namespace AuthenticationService.Repository
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