using AuthenticationService.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Shared.DTO;

namespace Ecommerce.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task MakeOrder(List<OrderItem> newItems, int userAggregateId)
        {
            var isProductAvailable = await CheckIfProductAreAvailable(newItems);
            await _shoppingCartRepository.MakeOrder(newItems, userAggregateId);
        }

        public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
        {
            return await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
        }

        public async Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId)
        {
            return await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
        }

        private Task<bool> CheckIfProductAreAvailable(List<OrderItem> items)
        {
            return Task.FromResult(true);
        }
    }
}
