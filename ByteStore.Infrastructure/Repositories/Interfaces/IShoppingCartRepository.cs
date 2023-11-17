using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Infrastructure.Repository.Interfaces;

public interface IShoppingCartRepository
{
    Task<BuyOrderStatus> BuyOrder(int userAggregateId);

    Task CreateShoppingCart(int userAggregateId);

    //Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId);
    Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
    Task<OrderStatus> MakeOrder(OrderItem itemToAdd, int shoppingCartId);
    Task RemoveProductFromCart(int userAggregateId, int productId);
}