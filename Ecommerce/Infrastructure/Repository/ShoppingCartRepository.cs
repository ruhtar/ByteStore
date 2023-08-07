using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateShoppingCart(int userAggregateId)
        {
            var json = JsonSerializer.Serialize(new List<OrderItem>(), new JsonSerializerOptions());
            var bytes = Encoding.ASCII.GetBytes(json);
            var shoppingCart = new ShoppingCart
            {
                UserAggregateId = userAggregateId,
                OrderItems = bytes
            };
            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);
            if (cart == null) { return null; }
            var orderItem = JsonSerializer.Deserialize<List<OrderItem>>(cart.OrderItems);
            return new ShoppingCartDto
            {
                UserAggregateId = userAggregateId,
                ShoppingCartId = cart.ShoppingCartId,
                OrderItems = orderItem
            };
        }

        public async Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId);
            if (cart == null) { return null; }
            var orderItem = JsonSerializer.Deserialize<List<OrderItem>>(cart.OrderItems);
            return new ShoppingCartDto
            {
                ShoppingCartId = shoppingCartId,
                UserAggregateId = cart.UserAggregateId,
                OrderItems = orderItem
            };
        }

        public async Task<OrderStatus> MakeOrder(OrderItem newItem, int userAggregateId)
        {
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);
            var orderItems = new List<OrderItem>();
            if (shoppingCart.OrderItems != null && shoppingCart.OrderItems.Length != 0)
            {
                orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
            }

            var existingItem = orderItems.FirstOrDefault(x => x.ProductId == newItem.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += newItem.Quantity;
            }
            else
            {
                orderItems.Add(newItem);
            }

            var json = JsonSerializer.Serialize(orderItems, new JsonSerializerOptions());
            var bytes = Encoding.ASCII.GetBytes(json);
            shoppingCart.OrderItems = bytes;
            await _context.SaveChangesAsync();
            return OrderStatus.Approved;
        }

        public async Task BuyOrder(int userAggregateId)
        {
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);
            if (shoppingCart == null) return;
            var orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
            foreach (var item in orderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                product.ProductQuantity -= item.Quantity;
                await _context.SaveChangesAsync();
                orderItems.Remove(item);
            }
            await _context.SaveChangesAsync();
        }

    }
}
