using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apresentation.Controllers
{
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public ShoppingCartController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("teste")]
        public async Task<OkResult> Index()
        {
            var lista = new List<OrderItem> {
                new OrderItem {
                    ProductId = 1,
                    Quantity = 3
                }
            };
            await _orderRepository.MakeOrder(lista, 5);
            return Ok();
        }
    }
}
