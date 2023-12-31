﻿using ByteStore.Domain;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Services.Interfaces;

public interface IShoppingCartService
{
    Task<BuyOrderStatus> BuyOrder(int userAggregateId);
    Task<ShoppingCartResponseDto?> GetShoppingCartByUserAggregateId(int userAggregateId);
    Task<OrderStatus?> MakeOrder(OrderItem itemToAdd, int userAggregateId);
    Task RemoveProductFromCart(int userAggregateId, int productId);
}