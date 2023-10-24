namespace ByteStore.Shared.DTO
{
    public class ProductDto
    {
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductQuantity { get; set; }
        public string? Description { get; set; }
        public double? Rate { get; set; }
    }
}
