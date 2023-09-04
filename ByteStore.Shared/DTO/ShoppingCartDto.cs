using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Shared.DTO
{
    public class ShoppingCartDto
    {
        public int UserAggregateId { get; set; }
        public int ShoppingCartId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
