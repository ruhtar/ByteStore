using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    // private readonly ICacheConfigs _cache;
    // private const string AllProductsKey = "AllProductsKeys";

    public ProductRepository(AppDbContext context) //, ICacheConfigs cache
    {
        _context = context;
        // _cache = cache;
    }

    public async Task<List<Product>> GetAllProducts(GetProductsInputPagination input)
    {
        //This is just for testing if cache is avaible.
        //var stopwatch = new Stopwatch();
        //stopwatch.Start();

        // var cache = await _cache.GetFromCacheAsync<List<Product>>(AllProductsKey);
        // if (cache != null)
        //     //This is just for testing if cache is avaible.
        //     //stopwatch.Stop();
        //     //Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalSeconds} seconds");
        //     return cache;
        var query = _context.Products.AsNoTracking();

        if (input.PageSize > 0 && input.PageIndex >= 0)
        {
            query = query.Skip(input.PageIndex * input.PageSize).Take(input.PageSize);
        }

        var products = await query.ToListAsync();

        //This is just for testing if cache is avaible.
        //Thread.Sleep(5000);
        // var cacheData = JsonSerializer.Serialize(products);
        // await _cache.SetAsync(AllProductsKey, cacheData);
        //stopwatch.Stop();
        //Console.WriteLine($"Execution Time: {stopwatch.Elapsed.TotalSeconds} seconds");
        
        return products;
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == id);
    }

    public async Task<Product> AddProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> UpdateProduct(int id, UpdateProductDto product)
    {
        var oldProduct = await _context.Products.FindAsync(id);

        if (oldProduct == null) return false;

        oldProduct.ImageStorageUrl = string.IsNullOrEmpty(product.ImageStorageUrl) ? oldProduct.ImageStorageUrl : product.ImageStorageUrl;
        oldProduct.ProductQuantity = product.ProductQuantity ?? oldProduct.ProductQuantity;
        oldProduct.Price = product.Price ?? oldProduct.Price;
        oldProduct.Name = string.IsNullOrEmpty(product.Name) ? oldProduct.Name : product.Name;
        oldProduct.Description = string.IsNullOrEmpty(product.Description) ? oldProduct.Description : product.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Review>?> GetReviews(int productId)
    {
        var product = await _context.Products
            .Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        return product?.Reviews.ToList();
    }

    public async Task<Review> CreateReview(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }
}