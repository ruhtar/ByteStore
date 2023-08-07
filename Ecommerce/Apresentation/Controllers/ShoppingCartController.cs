using AuthenticationService.Domain.Aggregates;
using Ecommerce.Application.Services;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apresentation.Controllers
{
    [Route("cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("order")]
        public async Task<ActionResult> AddToShoppingCart([FromBody] List<OrderItem> orderItems, int userId)
        {
            await _shoppingCartService.MakeOrder(orderItems, userId);
            return Ok("Sucess.");
        }

        [HttpGet("/user/{userAggregateId}/cart")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartByUserAggregateId([FromRoute] int userAggregateId)
        {
            var cart = await _shoppingCartService.GetShoppingCartByUserAggregateId(userAggregateId);
            if (cart == null) return Problem("The shopping cart of the current user is not available.");
            return Ok(cart);
        }

        [HttpGet("/cart/{shoppingCartId}")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartById([FromRoute] int shoppingCartId)
        {
            var cart = await _shoppingCartService.GetShoppingCartById(shoppingCartId);
            if (cart == null) return Problem("The shopping cart is not available.");
            return Ok(cart);
        }
    }
}
