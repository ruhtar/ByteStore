using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;
using Ecommerce.Shared.Utils;
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
            var shoppingCart = new ShoppingCart
            {
                UserAggregateId = userAggregateId,
                OrderItems = Utils.Serializer(new List<OrderItem>())
            };
            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();
        }

        public async Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId)
        {
            var cart = await _context.ShoppingCarts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId);

            if (cart == null) { return null; }
            var orderItem = JsonSerializer.Deserialize<List<OrderItem>>(cart.OrderItems);
            return new ShoppingCartDto
            {
                ShoppingCartId = shoppingCartId,
                UserAggregateId = cart.UserAggregateId,
                OrderItems = orderItem
            };
        }
        public async Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);

            if (shoppingCart == null) { return null; }
            var orderItems = new List<OrderItem>();

            if (shoppingCart.OrderItems != null && shoppingCart.OrderItems.Length != 0)
            {
                orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
            }
            return new ShoppingCartDto
            {
                UserAggregateId = userAggregateId,
                ShoppingCartId = shoppingCart.ShoppingCartId,
                OrderItems = orderItems
            };
        }

        public async Task<OrderStatus> MakeOrder(OrderItem newItem, int userAggregateId)
        {
            //TODO: refatorar para camada de serviço
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);

            var orderItems = new List<OrderItem>();

            if (shoppingCart!.OrderItems != null && shoppingCart.OrderItems.Length != 0)
            {
                orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
            }

            var existingItem = orderItems!.FirstOrDefault(x => x.ProductId == newItem.ProductId);

            //Primeira vez do item no carrinho
            if (existingItem == null)
            {
                existingItem = new OrderItem
                {
                    ProductId = newItem.ProductId,
                    Quantity = newItem.Quantity,
                };
                orderItems!.Add(existingItem);
            }
            else
            {
                existingItem.Quantity += newItem.Quantity;
            }

            if (existingItem.Quantity < 0) existingItem.Quantity = 0; 

            shoppingCart.OrderItems = Utils.Serializer(orderItems);
            await _context.SaveChangesAsync();
            return OrderStatus.Approved;
        }

        public async Task<BuyOrderStatus> BuyOrder(int userAggregateId)
        {
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);

            shoppingCart!.OrderItems = Utils.Serializer(new OrderItem());
            await _context.SaveChangesAsync();
            return BuyOrderStatus.Completed;
        }
    }
}
