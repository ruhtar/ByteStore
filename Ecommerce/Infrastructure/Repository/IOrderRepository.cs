using AuthenticationService.Domain.Entities;
using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Infrastructure.Repository
{
    public interface IOrderRepository
    {
        Task<Order> GetOrder(int shoppingCartId);
        Task MakeOrder(List<OrderItem> newItems, int shoppingCartId);
    }
}