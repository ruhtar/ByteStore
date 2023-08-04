using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure;
using AuthenticationService.Infrastructure.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Text.Json;

namespace AuthenticationService.Infrastructure.Repository
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

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var cache = await _cache.GetFromCacheAsync(AllProductsKey);
            if (cache != null)
            {
                stopwatch.Stop();

                Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalSeconds} seconds");
                return cache;
            }
            var products = await _context.Products.ToListAsync();

            //This is just for testing if cache is avaible.
            Thread.Sleep(5000);

            var cacheData = JsonSerializer.Serialize(products);
            await _cache.SetAsync(AllProductsKey, cacheData);

            stopwatch.Stop();

            Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalSeconds} seconds");
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
