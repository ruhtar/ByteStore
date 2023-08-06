using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure;
using Ecommerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder(int shoppingCartId)
        {
            var response = await _context.Orders.SingleOrDefaultAsync(x=>x.ShoppingCartId == shoppingCartId);
            return response;
        }

        public async Task MakeOrder(List<OrderItem> newItems, int shoppingCartId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.ShoppingCartId == shoppingCartId);
            var orderItems = new List<OrderItem>();
            if (order.OrderItems == null) 
            {
                order.OrderItems = new byte[0];
            }
            else {
                orderItems = JsonSerializer.Deserialize<List<OrderItem>>(order.OrderItems);
            }

            //Comparar as duas listas para Ids iguais
            //Caso o Id exista na orderItems list, adicionar/retirar as quantidades descritas no pedido.
            //Caso o Id não exista, adicionar o Id e as quantidades descritas (caso sejam positivas. negativas = fila de espera)

            foreach (var item in newItems)
            {
                var isItemInOrder = orderItems.Any(x => x.ProductId == item.ProductId);
                if (isItemInOrder)
                {
                    var existingItem = orderItems.SingleOrDefault(x => x.ProductId == item.ProductId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += item.Quantity;
                    }
                }
                else
                {
                    orderItems.Add(item);
                }
            }

            var json = JsonSerializer.Serialize(orderItems, new JsonSerializerOptions());
            var bytes = Encoding.ASCII.GetBytes(json);
            order.OrderItems = bytes;
            await _context.SaveChangesAsync();
        }
    }
}
