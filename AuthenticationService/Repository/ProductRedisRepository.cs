using AuthenticationService.Data;
using AuthenticationService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AuthenticationService.Repository
{
    public class ProductRedisRepository : IProductRedisRepository
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private const string AllProductsKey = "AllProductsKeys";
        private const string ProductByIdKey = "ProductByIdKey";
        private const string CreateProductKey = "CreateProductKey";
        private const string UpdateProductKey = "UpdateProductKey";
        private const string DeleteProductKey = "DeleteProductKey";

        public ProductRedisRepository(AppDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _cache = memoryCache;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = _cache.GetOrCreate(AllProductsKey, async entry =>
            {

                //Relative expiration => If not requested, will expire in the given time.
                entry.SlidingExpiration = TimeSpan.FromSeconds(7);
                //Absolute expiration => Will expire in the given time, no matter if it was requested or not.
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15);
                entry.SetPriority(CacheItemPriority.High);

                Thread.Sleep(5000);

                return await _context.Products.ToListAsync();
            });
            return await products;
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
            _cache.Remove(AllProductsKey);
            return true;
        }
    }
}
