using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Cache;
using Ecommerce.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ICacheConfigs _cache;
        private const string AllProductsKey = "AllProductsKeys";

        public ProductRepository(AppDbContext context, ICacheConfigs cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();

            var cache = await _cache.GetFromCacheAsync(AllProductsKey);
            if (cache != null)
            {
                //stopwatch.Stop();

                //Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalSeconds} seconds");
                return cache;
            }
            var products = await _context.Products.AsNoTracking().ToListAsync();

            //This is just for testing if cache is avaible.
            //Thread.Sleep(5000);

            var cacheData = JsonSerializer.Serialize(products);
            await _cache.SetAsync(AllProductsKey, cacheData);

            //stopwatch.Stop();

            //Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalSeconds} seconds");
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var oldProduct = await _context.Products.FindAsync(id);

            if (oldProduct == null) return null;

            oldProduct.ProductQuantity = product.ProductQuantity;
            oldProduct.Price = product.Price;
            oldProduct.Name = product.Name;

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
