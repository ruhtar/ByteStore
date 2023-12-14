using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public UserRepository(AppDbContext context, IShoppingCartRepository shoppingCartRepository)
    {
        _context = context;
        _shoppingCartRepository = shoppingCartRepository;
    }

    public async Task RegisterUser(UserAggregate userAggregate)
    {
        await _context.UserAggregates.AddAsync(userAggregate);
        await _context.SaveChangesAsync();
        await _shoppingCartRepository.CreateShoppingCart(userAggregate.UserAggregateId);
    }

    public async Task<ChangePasswordStatusResponse> UpdateUserPassword(int userId, string hashedPassword)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null) return ChangePasswordStatusResponse.UserNotFound;
        user.Password = hashedPassword;
        await _context.SaveChangesAsync();
        return ChangePasswordStatusResponse.Sucess;
    }

    public async Task<UserAggregate?> GetUserAggregateByUsername(string username)
    {
        return await _context.UserAggregates
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(u => u.User.Username == username);
    }

    public async Task EditUserAddress(Address address, int userId)
    {
        var user = await _context.UserAggregates.FirstOrDefaultAsync(u => u.UserAggregateId == userId);
        if (user == null) return;
        user.Address = address;
        await _context.SaveChangesAsync();
    }

    public async Task<Address?> GetUserAddress(int userId)
    {
        var user = await _context.UserAggregates.AsNoTracking().FirstOrDefaultAsync(u => u.UserAggregateId == userId);
        return user?.Address;
    }

    public async Task<string?> GetUserPurchaseHistory(int userId)
    {
        var user = await _context.UserAggregates.AsNoTracking().FirstOrDefaultAsync(u => u.UserAggregateId == userId);
        return user?.PurchaseHistory;
    }

    public async Task<bool> CheckIfUserHasBoughtAProduct(int userId, int productId)
    {
        var user = await _context.UserAggregates.AsNoTracking().FirstOrDefaultAsync(u => u.UserAggregateId == userId);
        if (user == null) return false;

        var purchasedProducts = user.GetPurchaseHistory();
        return purchasedProducts.Any(x => x.Product.ProductId == productId);
    }

    public async Task UpdatePurchaseHistory(int userId, IEnumerable<Product> purchasedProducts)
    {
        var user = await _context.UserAggregates.FirstOrDefaultAsync(u => u.UserAggregateId == userId);
        if (user == null) return;
        var purchasedProductDetails = purchasedProducts.Select(x => new PurchasedProductDetail()
        {
            Product = x,
            PurchaseDate = DateTime.Now
        }).ToList();
        user.AddToPurchaseHistory(purchasedProductDetails);
        await _context.SaveChangesAsync();
    }

    //public async Task<User> GetUserByUsername() {
    //    await _context.UserAggregates.AsNoTracking().FirstOrDefaultAsync(u => u.User.Username);
    //}
}