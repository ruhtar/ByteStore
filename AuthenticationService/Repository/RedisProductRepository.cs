using AuthenticationService.Cache;
using AuthenticationService.Data;
using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AuthenticationService.Repository
{
    public class RedisProductRepository : IRedisProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ICacheConfigs _cache;
        private const string AllProductsKey = "AllProductsKeys";

        public RedisProductRepository(AppDbContext context, ICacheConfigs cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var cache = await _cache.GetFromCacheAsync(AllProductsKey);
            if (cache != null) return cache;
            var products = await _context.Products.ToListAsync();

            Thread.Sleep(5000);
            
            var cacheData = JsonSerializer.Serialize(products);
            await _cache.SetAsync(AllProductsKey, cacheData);

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
