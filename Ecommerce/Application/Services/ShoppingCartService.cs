using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Infrastructure.Repository;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Shared.DTO;

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

        public async Task MakeOrder(List<OrderItem> newItems, int userAggregateId)
        {
            //var availableProducts = new List<OrderItem>();
            //foreach (var item in newItems)
            //{
            //    var isProductAvailable = await CheckIfProductAreAvailable(item);
            //    if(isProductAvailable) availableProducts.Add(item);
            //}
            await _shoppingCartRepository.MakeOrder(newItems, userAggregateId);
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
