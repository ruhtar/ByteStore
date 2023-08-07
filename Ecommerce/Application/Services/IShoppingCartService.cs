using AuthenticationService.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;

namespace Ecommerce.Application.Services
{
    public interface IShoppingCartService
    {
        Task BuyOrder(int userAggregateId);
        Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId);
        Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
        Task MakeOrder(List<OrderItem> newItems, int userAggregateId);
    }
}