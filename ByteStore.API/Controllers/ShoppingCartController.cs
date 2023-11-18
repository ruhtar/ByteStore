using ByteStore.API.Attributes;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ByteStore.API.Controllers;

[Authorize]
[Route("cart")]
[ApiController]
[TokenValidation]
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
        // if (!IsTokenValid()) return Unauthorized();
        var result = await _shoppingCartService.MakeOrder(item, userId);
        if (result == OrderStatus.Approved) return Ok(result.ToString());
        if (result == OrderStatus.InvalidQuantity) return BadRequest(result.ToString());
        return BadRequest(result.ToString());
    }

    [HttpGet("buy")]
    public async Task<ActionResult> BuyOrder(int userId)
    {
        // if (!IsTokenValid()) return Unauthorized();
        var result = await _shoppingCartService.BuyOrder(userId);
        if (result == BuyOrderStatus.InvalidQuantity) return BadRequest(result.ToString());
        return Ok(BuyOrderStatus.Completed.ToString());
    }
    
    [HttpGet("user/{userAggregateId}")]
    public async Task<ActionResult<ShoppingCartResponseDto>> GetShoppingCartByUserAggregateId(
        [FromRoute] int userAggregateId)
    {
        // if (!IsTokenValid()) return Unauthorized();
        var cart = await _shoppingCartService.GetShoppingCartByUserAggregateId(userAggregateId);
        if (cart == null) return Problem("The shopping cart of the current user is not available.");
        return Ok(cart);
    }

    [HttpDelete("user/{userAggregateId}/remove")]
    public async Task<IActionResult> RemoveProductFromShoppingCart(int userAggregateId, int productId)
    {
        // if (!IsTokenValid()) return Unauthorized();
        await _shoppingCartService.RemoveProductFromCart(userAggregateId, productId);
        return Ok();
    }
}