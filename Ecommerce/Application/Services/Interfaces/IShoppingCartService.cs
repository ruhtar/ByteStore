using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<BuyOrderStatus> BuyOrder(int userAggregateId);
        Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId);
        Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
        Task<OrderStatus> MakeOrder(OrderItem item, int userAggregateId);
    }
}