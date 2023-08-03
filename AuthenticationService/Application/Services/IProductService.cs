using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(Product product);
    }
}