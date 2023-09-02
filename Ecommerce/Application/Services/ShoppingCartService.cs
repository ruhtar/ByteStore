using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Infrastructure.Repository.Interfaces;

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

        public async Task<BuyOrderStatus> BuyOrder(int userAggregateId)
        {
            var shoppingCartDto = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);

            foreach (var item in shoppingCartDto!.OrderItems)
            {
                var product = await _productRepository.GetProductById(item.ProductId);
                if (product.ProductQuantity >= item.Quantity)
                {
                    product.ProductQuantity -= item.Quantity;
                    await _productRepository.UpdateProduct(product.ProductId, product);
                }
                else
                {
                    return BuyOrderStatus.InvalidQuantity;                        
                }
            }

            return await _shoppingCartRepository.BuyOrder(userAggregateId);
        }

        public async Task<OrderStatus> MakeOrder(OrderItem item, int userAggregateId)
        {
            var product = await _productRepository.GetProductById(item.ProductId);

            if (product == null)
                return OrderStatus.ProductNotFound;

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);

            if (shoppingCart == null)
                return OrderStatus.ProductNotFound;

            var orderItem = shoppingCart.OrderItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (orderItem == null) 
            {
                orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = 0,
                };
            }

            if (product.ProductQuantity < orderItem.Quantity + item.Quantity)
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
    }
}
