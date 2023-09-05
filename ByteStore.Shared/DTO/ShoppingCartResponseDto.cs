namespace ByteStore.Shared.DTO
{
    public class ShoppingCartResponseDto
    {
        public int UserAggregateId { get; set; }
        public int ShoppingCartId { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
