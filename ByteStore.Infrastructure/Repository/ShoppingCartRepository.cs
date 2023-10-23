using System.Text.Json;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using ByteStore.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace ByteStore.Infrastructure.Repository
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

        //public async Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId)
        //{
        //    var cart = await _context.ShoppingCarts
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId);

        //    if (cart == null) { return null; }
        //    var orderItem = JsonSerializer.Deserialize<List<OrderItem>>(cart.OrderItems);
        //    return new ShoppingCartDto
        //    {
        //        ShoppingCartId = shoppingCartId,
        //        UserAggregateId = cart.UserAggregateId,
        //        OrderItems = orderItem
        //    };
        //}
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

        public async Task<OrderStatus> MakeOrder(OrderItem itemToAdd, int userAggregateId)
        {
            //TODO: refatorar para camada de serviço
            var shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserAggregateId == userAggregateId);

            var orderItems = new List<OrderItem>();

            if (shoppingCart!.OrderItems != null && shoppingCart.OrderItems.Length != 0)
            {
                orderItems = JsonSerializer.Deserialize<List<OrderItem>>(shoppingCart.OrderItems);
            }

            var existingItem = orderItems!.FirstOrDefault(x => x.ProductId == itemToAdd.ProductId);

            //Primeira vez do item no carrinho
            if (existingItem == null)
            {
                existingItem = new OrderItem
                {
                    ProductId = itemToAdd.ProductId,
                    Quantity = itemToAdd.Quantity,
                };
                orderItems!.Add(existingItem);
            }
            else
            {
                existingItem.Quantity += itemToAdd.Quantity;
            }

            if (existingItem.Quantity < 0) existingItem.Quantity = 0; 

            shoppingCart.OrderItems = Utils.Serializer(orderItems);
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
}
