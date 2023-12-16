using ByteStore.Domain.Aggregates;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Infrastructure.Repositories.Interfaces;

public interface IShoppingCartRepository
{
    Task<BuyOrderStatus> BuyOrder(int userAggregateId);

    Task CreateShoppingCart(UserAggregate userAggregate);

    //Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId);
    Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
    Task<OrderStatus?> MakeOrder(int shoppingCartId, byte[] data);
    Task RemoveProductFromCart(int userAggregateId, int productId);
}