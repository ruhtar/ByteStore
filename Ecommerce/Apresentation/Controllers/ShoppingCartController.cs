using Ecommerce.Domain.Aggregates;
using Ecommerce.Application.Services;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;
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
            //vai até a tabela dos produtos 
            //precisa checar novamente se as quantidades são válidas e retira as quantidades
            //zera os produtos do carrinho do user
            //ao finalizar a compra, a operação inteira precisa ser feita, ou seja, TODOS os produtos devem estar válidos para a compra ser efeutada.
            //Retornar algo do tipo: Erro 500("Invalid buy order. Product 'id' avaiable quantity: X. Ordered quantity: Y")
            var result = await _shoppingCartService.BuyOrder(userId);
            if(result == BuyOrderStatus.InvalidQuantity) return BadRequest(result.ToString());
            return Ok(BuyOrderStatus.Completed.ToString());
        }

        [HttpGet("/user/{userAggregateId}/cart")]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCartByUserAggregateId([FromRoute] int userAggregateId)
        {
            var cart = await _shoppingCartService.GetShoppingCartByUserAggregateId(userAggregateId);
            if (cart == null) return Problem("The shopping cart of the current user is not available.");
            return Ok(cart);
        }

        [HttpGet("/cart/{shoppingCartId}")]
        public async Task<ActionResult<ShoppingCartDto?>> GetShoppingCartById([FromRoute] int shoppingCartId)
        {
            var cart = await _shoppingCartService.GetShoppingCartById(shoppingCartId);
            if (cart == null) return Problem("The shopping cart is not available.");
            return Ok(cart);
        }
    }
}
