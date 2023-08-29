using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(int id, Product product);
    }
}