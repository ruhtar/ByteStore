using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;

namespace ByteStore.Application.Services.Interfaces;

public interface IProductService
{
    Task<Product> AddProduct(ProductDto product);
    Task<bool> DeleteProduct(int id);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(int id);
    Task<bool> UpdateProduct(int id, UpdateProductDto productDto);
}