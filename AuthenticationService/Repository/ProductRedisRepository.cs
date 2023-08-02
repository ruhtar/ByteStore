using AuthenticationService.Cache;
using AuthenticationService.Data;
using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AuthenticationService.Repository
{
    public class ProductRedisRepository : IProductRedisRepository
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _redis;
        private const string AllProductsKey = "AllProductsKeys";
        private const string ProductByIdKey = "ProductByIdKey";
        private const string CreateProductKey = "CreateProductKey";
        private const string UpdateProductKey = "UpdateProductKey";
        private const string DeleteProductKey = "DeleteProductKey";

        public ProductRedisRepository(AppDbContext context, IDistributedCache redis)
        {
            _context = context;
            _redis = redis;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var cache = CacheConfigs.GetFromCache(AllProductsKey);
            if (cache != null) return cache;
            var products = await _context.Products.ToListAsync();

            Thread.Sleep(5000);
            
            var cacheData = JsonSerializer.Serialize(products);
            _redis.SetString(AllProductsKey, cacheData, CacheConfigs.GetCacheOptions());

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // Clear the "AllProductsCache" key after deleting the product
            return true;
        }
    }
}
