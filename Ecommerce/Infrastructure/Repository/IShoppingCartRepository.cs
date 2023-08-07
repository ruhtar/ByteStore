using AuthenticationService.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;

namespace Ecommerce.Infrastructure.Repository
{
    public interface IShoppingCartRepository
    {
        Task BuyOrder(int userAggregateId);
        Task CreateShoppingCart(int userAggregateId);
        Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId);
        Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
        Task<OrderStatus> MakeOrder(OrderItem newItem, int shoppingCartId);
    }
}