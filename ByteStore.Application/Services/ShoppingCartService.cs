using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Infrastructure.Cache;

namespace ByteStore.Application.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICacheConfigs _cache;
    private const string CartItemKey = "CartItemKey";


    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository,
        ICacheConfigs cache)
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
            if (IsProductQuantityValidToBuy(product.ProductQuantity, item.Quantity))
            {
                product.ProductQuantity -= item.Quantity;
                if (product.ProductQuantity == 0)
                    await _productRepository.DeleteProduct(product.ProductId);
                else
                    await _productRepository.UpdateProduct(product.ProductId, product);
            }
            else
            {
                return BuyOrderStatus.InvalidQuantity;
            }
        }

        return await _shoppingCartRepository.BuyOrder(userAggregateId);
    }

    public async Task<OrderStatus> MakeOrder(OrderItem itemToAdd, int userAggregateId)
    {
        var availableProduct = await _productRepository.GetProductById(itemToAdd.ProductId);

        if (availableProduct == null)
            return OrderStatus.ProductNotFound;

        var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);

        if (shoppingCart == null)
            return OrderStatus.ProductNotFound;

        var cartOrderItem = shoppingCart.OrderItems.FirstOrDefault(x => x.ProductId == itemToAdd.ProductId);
        if (cartOrderItem == null)
            cartOrderItem = new OrderItem
            {
                ProductId = itemToAdd.ProductId,
                Quantity = 0
            };

        if (availableProduct.ProductQuantity < cartOrderItem.Quantity + itemToAdd.Quantity)
            return OrderStatus.InvalidQuantity;

        return await _shoppingCartRepository.MakeOrder(itemToAdd, userAggregateId);
    }

    //public async Task<ShoppingCartDto?> GetShoppingCartById(int shoppingCartId)
    //{
    //    return await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
    //}

    public async Task<ShoppingCartResponseDto> GetShoppingCartByUserAggregateId(int userAggregateId)
    {
        var cart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
        if (cart == null) return null;
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
                item.Quantity = EnsureProductQuantityIsAvailable(item.Quantity, product.ProductQuantity);
                var productDto = new ProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    ProductQuantity = item.Quantity
                };
                products.Add(productDto);
            }
        }

        cartDto.Products = products;
        //var cacheData = JsonSerializer.Serialize(cartDto);
        //await _cache.SetAsync(CartItemKey, cacheData);
        return cartDto;
    }

    public async Task RemoveProductFromCart(int userAggregateId, int productId)
    {
        await _shoppingCartRepository.RemoveProductFromCart(userAggregateId, productId);
    }

    private static bool IsProductQuantityValidToBuy(int productQuantity, int itemQuantity)
    {
        return productQuantity >= itemQuantity;
    }

    private int EnsureProductQuantityIsAvailable(int currentProductQuantity, int availableProductQuantity)
    {
        if (currentProductQuantity > availableProductQuantity) currentProductQuantity = availableProductQuantity;

        return currentProductQuantity;
    }
}