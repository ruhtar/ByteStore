using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure.Repository;

namespace AuthenticationService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRedisProductRepository _productRepository;

        public ProductService(IRedisProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }
    }
}
