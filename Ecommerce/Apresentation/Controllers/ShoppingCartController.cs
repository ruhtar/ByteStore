using AuthenticationService.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apresentation.Controllers
{
    [Route("cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost("/order")]
        public async Task<OkResult> AddToShoppingCart([FromBody] List<OrderItem> orderItems, int userId)
        {
            await _shoppingCartRepository.MakeOrder(orderItems, userId);
            return Ok();
        }

        [HttpGet("/user/{userAggregateId}/cart")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartByUserAggregateId([FromRoute] int userAggregateId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCartByUserAggregateId(userAggregateId);
            if (cart == null) return Problem("The shopping cart of the current user is not available.");
            return Ok(cart);
        }

        [HttpGet("/cart/{shoppingCartId}")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartById([FromRoute] int shoppingCartId)
        {
            var cart = await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
            if (cart == null) return Problem("The shopping cart is not available.");
            return Ok(cart);
        }
    }
}
