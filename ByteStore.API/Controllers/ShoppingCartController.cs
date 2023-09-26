using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ByteStore.API.Controllers
{
    [Route("cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ITokenService _tokenService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ITokenService tokenService)
        {
            _shoppingCartService = shoppingCartService;
            _tokenService = tokenService;
        }

        [HttpPost("order")]
        public async Task<ActionResult<OrderStatus>> MakeOrder(OrderItem item, int userId)
        {
            var result = await _shoppingCartService.MakeOrder(item, userId);
            if (result == OrderStatus.Approved) return Ok(result.ToString());
            if (result == OrderStatus.InvalidQuantity) return Problem(result.ToString());
            return BadRequest(result.ToString());
        }

        [HttpGet("buy")]
        public async Task<ActionResult> BuyOrder(int userId)
        {
            var result = await _shoppingCartService.BuyOrder(userId);
            if(result == BuyOrderStatus.InvalidQuantity) return BadRequest(result.ToString());
            return Ok(BuyOrderStatus.Completed.ToString());
        }

        [HttpGet("/user/{userAggregateId}/cart")]
        public async Task<ActionResult<ShoppingCartResponseDto>> GetShoppingCartByUserAggregateId([FromRoute] int userAggregateId)
        {
            var cart = await _shoppingCartService.GetShoppingCartByUserAggregateId(userAggregateId);
            if (cart == null) return Problem("The shopping cart of the current user is not available.");
            return Ok(cart);
        }

        //[HttpGet("/cart/{shoppingCartId}")]
        //public async Task<ActionResult<ShoppingCartDto?>> GetShoppingCartById([FromRoute] int shoppingCartId)
        //{
        //    var cart = await _shoppingCartService.GetShoppingCartById(shoppingCartId);
        //    if (cart == null) return Problem("The shopping cart is not available.");
        //    return Ok(cart);
        //}
    }
}
