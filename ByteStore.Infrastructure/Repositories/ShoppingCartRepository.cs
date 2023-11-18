using System.Text.Json;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using ByteStore.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateShoppingCart(int userAggregateId)
    {
        var shoppingCart = new ShoppingCart
        {
            UserAggregateId = userAggregateId,
            OrderItems = Utils.Serializer(new List<OrderItem>())
        };
        await _context.ShoppingCarts.AddAsync(shoppingCart);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId)
    {
        var shoppingCart = await _context.ShoppingCarts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);

        if (shoppingCart == null) return null;
        var orderItems = new List<OrderItem>();

        if (shoppingCart.OrderItems != null && shoppingCart.OrderItems.Length != 0)
            orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
        return new ShoppingCartDto
        {
            UserAggregateId = userAggregateId,
            ShoppingCartId = shoppingCart.ShoppingCartId,
            OrderItems = orderItems
        };
    }

    public async Task<OrderStatus?> MakeOrder(int userAggregateId, byte[] data)
    {
        var cart = await _context.ShoppingCarts
            .FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);
        if (cart == null) return null;
        cart.OrderItems = data;
        await _context.SaveChangesAsync();
        return OrderStatus.Approved;
    }

    public async Task<BuyOrderStatus> BuyOrder(int userAggregateId)
    {
        var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);

        shoppingCart!.OrderItems = Utils.Serializer(new List<OrderItem>());
        await _context.SaveChangesAsync();
        return BuyOrderStatus.Completed;
    }

    public async Task RemoveProductFromCart(int userAggregateId, int productId)
    {
        var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);
        if (shoppingCart == null) return;
        var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
        if (orderItems != null)
        {
            var itemToRemove = orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (itemToRemove != null) orderItems.Remove(itemToRemove);
            shoppingCart.OrderItems = Utils.Serializer(orderItems);
            await _context.SaveChangesAsync();
        }
    }
}