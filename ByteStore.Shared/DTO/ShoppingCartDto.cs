using ByteStore.Domain.ValueObjects;

namespace ByteStore.Shared.DTO
{
    public class ShoppingCartDto
    {
        public int UserAggregateId { get; set; }
        public int ShoppingCartId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
