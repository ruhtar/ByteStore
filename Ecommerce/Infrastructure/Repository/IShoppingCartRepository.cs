using AuthenticationService.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;

namespace Ecommerce.Infrastructure.Repository
{
    public interface IShoppingCartRepository
    {
        Task CreateShoppingCart(int userAggregateId);
        Task<ShoppingCart> GetShoppingCartById(int shoppingCartId);
        Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
        Task MakeOrder(List<OrderItem> newItems, int shoppingCartId);
    }
}