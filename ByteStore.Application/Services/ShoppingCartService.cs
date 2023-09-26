using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Infrastructure.Repository.Interfaces;
using System.Text.Json;
using ByteStore.Infraestructure.Cache;

namespace ByteStore.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICacheConfigs _cache;
        private const string CartItemKey = "CartItemKey";


        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, ICacheConfigs cache)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _cache = cache;
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

        //public async Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId)
        //{
        //    return await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
        //}

        public async Task<ShoppingCartResponseDto> GetShoppingCartByUserAggregateId(int userAggregateId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
            var cartDto = new ShoppingCartResponseDto
            {
                UserAggregateId = cart.UserAggregateId,
                ShoppingCartId = cart.ShoppingCartId,
                Products = null
            };
            //var cache = await _cache.GetFromCacheAsync<ShoppingCartResponseDto>(CartItemKey);
            //if (cache != null)
            //{
                //cartDto.Products = cache.Products.Select(c => new RequestProductDto
                //{
                //    Name = c.Name,
                //    Price = c.Price,
                //    ProductQuantity = c.ProductQuantity,
                //}).ToList();
            //    return cache;
            //}

            var products = new List<ProductDto>();

            foreach (var item in cart.OrderItems)
            {
                var product = await _productRepository.GetProductById(item.ProductId);

                if (product != null)
                {
                    var productDto = new ProductDto
                    {
                        Name = product.Name,
                        Price = product.Price,
                        ProductQuantity = item.Quantity,
                    };
                    products.Add(productDto);
                }
            }
            cartDto.Products = products;
            //var cacheData = JsonSerializer.Serialize(cartDto);
            //await _cache.SetAsync(CartItemKey, cacheData);
            return cartDto;
        }
    }
}
