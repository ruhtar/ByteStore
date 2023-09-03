using Ecommerce.Domain.ValueObjects;

namespace Ecommerce.Shared.DTO
{
    public class ShoppingCartResponseDto
    {
        public int UserAggregateId { get; set; }
        public int ShoppingCartId { get; set; }
        public List<RequestProductDto> Products { get; set; }
    }
}
