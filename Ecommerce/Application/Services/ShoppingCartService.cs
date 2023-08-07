using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Infrastructure.Repository;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;

namespace Ecommerce.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        public async Task BuyOrder(int userAggregateId) {
            await _shoppingCartRepository.BuyOrder( userAggregateId);
        }

        public async Task<OrderStatus> MakeOrder(OrderItem item, int userAggregateId)
        {
            var product = await _productRepository.GetProductById(item.ProductId);

            if (product == null) 
                return OrderStatus.ProductNotFound;

            if (product.ProductQuantity < item.Quantity) 
                return OrderStatus.InvalidQuantity;

            return await _shoppingCartRepository.MakeOrder(item, userAggregateId);
        }

        public async Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId)
        {
            return await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
        }

        public async Task<ShoppingCartDto?> GetShoppingCartByUserAggregateId(int userAggregateId)
        {
            return await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
        }

        //private async Task<bool> CheckIfProductAreAvailable(OrderItem item)
        //{
        //    var product = await _productRepository.GetProductById(item.Product.ProductId);
        //    if (product == null || product.ProductQuantity < item.Quantity) return false;
        //    return true;
        //}
    }
}
