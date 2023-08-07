using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
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
                OrderItems = orderItem
            };
        }

        public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
        {
            return await _context.ShoppingCarts.SingleOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId);
        }

        public async Task MakeOrder(List<OrderItem> newItems, int userAggregateId)
        {
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);
            var orderItems = new List<OrderItem>();
            if (shoppingCart.OrderItems != null && shoppingCart.OrderItems.Length != 0)
            {
                orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
            }

            //TODO: Checar se todos os produtos existem e ESTÃO DISPONÍVEIS


            foreach (var newItem in newItems)
            {
                var existingItem = orderItems.FirstOrDefault(x => x.Product.ProductId == newItem.Product.ProductId);

                if (existingItem != null)
                {
                    existingItem.Quantity += newItem.Quantity;
                }
                else
                {
                    orderItems.Add(newItem);
                }
            }

            var json = JsonSerializer.Serialize(orderItems, new JsonSerializerOptions());
            var bytes = Encoding.ASCII.GetBytes(json);
            shoppingCart.OrderItems = bytes;
            await _context.SaveChangesAsync();
        }

    }
}
