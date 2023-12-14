using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain;
using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.Utils;

namespace ByteStore.Application.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository,
        IUserRepository userRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
    }

    public async Task<BuyOrderStatus> BuyOrder(int userAggregateId)
    {
        var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
        var itemsBought = new List<Product>();
        foreach (var itemToBeBought in shoppingCart!.OrderItems)
        {
            var stockedProduct = await _productRepository.GetProductById(itemToBeBought.ProductId);
            if (stockedProduct == null)
            {
                //this means that the product have been brought while it was in the user`s cart.
                //So it must be removed from the cart
                await _shoppingCartRepository.RemoveProductFromCart(userAggregateId, itemToBeBought.ProductId); 
                //TODO: notify the frontend of this operation
                continue;
            }

            if (IsProductQuantityValidToBuy(stockedProduct.ProductQuantity, itemToBeBought.Quantity))
            {
                stockedProduct.ProductQuantity -= itemToBeBought.Quantity;
                if (stockedProduct.ProductQuantity == 0)
                    await _productRepository.DeleteProduct(stockedProduct.ProductId);
                else
                {
                    var updateProductDto = new UpdateProductDto
                    {
                        ProductId = stockedProduct.ProductId,
                        Name = stockedProduct.Name,
                        Price = stockedProduct.Price,
                        ProductQuantity = stockedProduct.ProductQuantity,
                        Image = null,
                        ImageStorageUrl = stockedProduct.ImageStorageUrl,
                        Description = stockedProduct.Description
                    };
                    itemsBought.Add(new Product
                    {
                        ProductId = itemToBeBought.ProductId,
                        ProductQuantity = itemToBeBought.Quantity,
                        Name = stockedProduct.Name,
                        Price = stockedProduct.Price,
                        ImageStorageUrl = stockedProduct.ImageStorageUrl,
                        Description = stockedProduct.Description,
                    });
                    await _productRepository.UpdateProduct(stockedProduct.ProductId, updateProductDto);
                }
               
            }
            else
            {
                return BuyOrderStatus.InvalidQuantity;
            }
        }

        await _userRepository.UpdatePurchaseHistory(userAggregateId, itemsBought);
        return await _shoppingCartRepository.BuyOrder(userAggregateId);
    }

    public async Task<OrderStatus?> MakeOrder(OrderItem itemToAdd, int userAggregateId)
    {
        var availableProduct = await _productRepository.GetProductById(itemToAdd.ProductId);
        
        var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);

        if (availableProduct == null || shoppingCart == null)
            return OrderStatus.ProductNotFound;

        var cartOrderItem = shoppingCart.OrderItems.FirstOrDefault(x => x.ProductId == itemToAdd.ProductId);

        if (cartOrderItem == null) //this means that is the first time the item is in the user`s cart
        {
            cartOrderItem = new OrderItem
            {
                ProductId = itemToAdd.ProductId,
                Quantity = 0 
            };
            shoppingCart.OrderItems.Add(cartOrderItem);
        } 

        if (availableProduct.ProductQuantity < cartOrderItem.Quantity + itemToAdd.Quantity)
            return OrderStatus.InvalidQuantity;
  
        cartOrderItem.Quantity += itemToAdd.Quantity;
        if (cartOrderItem.Quantity < 0) cartOrderItem.Quantity = 0;
        

        var data = Utils.Serializer(shoppingCart.OrderItems);
        
        return await _shoppingCartRepository.MakeOrder(userAggregateId, data);
    }

    public async Task<ShoppingCartResponseDto?> GetShoppingCartByUserAggregateId(int userAggregateId)
    {
        var cart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
        if (cart == null) return null;
        var cartDto = new ShoppingCartResponseDto
        {
            UserAggregateId = cart.UserAggregateId,
            ShoppingCartId = cart.ShoppingCartId,
            Products = null
        };

        var products = new List<ProductDto>();

        foreach (var item in cart.OrderItems)
        {
            var product = await _productRepository.GetProductById(item.ProductId);

            if (product == null) continue;
            
            item.Quantity = EnsureProductQuantityIsAvailable(item.Quantity, product.ProductQuantity);
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                ProductQuantity = item.Quantity,
                Description = product.Description
            };
            products.Add(productDto);
        }

        cartDto.Products = products;
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

    private static int EnsureProductQuantityIsAvailable(int currentProductQuantity, int availableProductQuantity)
    {
        if (currentProductQuantity > availableProductQuantity) 
            currentProductQuantity = availableProductQuantity;

        return currentProductQuantity;
    }
}