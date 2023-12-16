using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Cache;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly ICacheService _cache;

    public ProductRepository(AppDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<Product?>> GetAllProducts(GetProductsInputPagination input)
    {
        var query = _context.Products.AsNoTracking();

        if (input.PageSize > 0 && input.PageIndex >= 0)
        {
            query = query.Skip(input.PageIndex * input.PageSize).Take(input.PageSize);
        }

        var products = await query.ToListAsync();

        return products;
    }

    public async Task<Product?> GetProductById(int id)
    {
        var cacheKey = $"Product:{id}";
        var cache = await _cache.GetFromCacheAsync<Product>(cacheKey);
        if (cache != null) return cache;

        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ProductId == id);

        await _cache.SetAsync(cacheKey, product);

        return product;
    }

    public async Task<Product?> AddProduct(Product? product)
    {
        await _context.Products.AddAsync(product);
        var changes = await _context.SaveChangesAsync();
        return changes > 0 ? product : null;
    }

    public async Task<bool> UpdateProduct(int id, UpdateProductDto product)
    {
        Product oldProduct;
        var cacheKey = $"Product:{id}";
        var cache = await _cache.GetFromCacheAsync<Product>(cacheKey);
        if (cache != null)
        {
            oldProduct = cache;
            _context.Attach(oldProduct);
        }
        else
        {
            oldProduct = await _context.Products.FindAsync(id);
        }

        if (oldProduct == null) return false;

        oldProduct.ImageStorageUrl = string.IsNullOrEmpty(product.ImageStorageUrl)
            ? oldProduct.ImageStorageUrl
            : product.ImageStorageUrl;
        oldProduct.ProductQuantity = product.ProductQuantity ?? oldProduct.ProductQuantity;
        oldProduct.Price = product.Price ?? oldProduct.Price;
        oldProduct.Name = string.IsNullOrEmpty(product.Name) ? oldProduct.Name : product.Name;
        oldProduct.Description =
            string.IsNullOrEmpty(product.Description) ? oldProduct.Description : product.Description;
        oldProduct.TimesRated = product.TimesRated ?? oldProduct.TimesRated;
        oldProduct.Rate = product.Rate ?? oldProduct.Rate;

        var changes = await _context.SaveChangesAsync();

        await _cache.SetAsync(cacheKey, oldProduct);

        return changes > 0;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var cacheKey = $"Product:{id}";
        var cache = await _cache.GetFromCacheAsync<Product>(cacheKey);
        Product product;
        if (cache != null)
        {
            product = cache;
            _context.Attach(product);
        }
        else
        {
            product = await _context.Products.FindAsync(id);
        }

        if (product == null) return false;

        _context.Products.Remove(product);
        var changes = await _context.SaveChangesAsync();
        await _cache.Invalidate(cacheKey);
        return changes > 0;
    }

    public async Task<List<Review>?> GetReviews(int productId)
    {
        var product = await _context.Products
            .Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        return product?.Reviews.ToList();
    }

    public async Task<Review?> CreateReview(Review? review)
    {
        await _context.Reviews.AddAsync(review);
        var changes = await _context.SaveChangesAsync();
        return changes > 0 ? review : null;
    }
}