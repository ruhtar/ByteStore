using Microsoft.AspNetCore.Http;

namespace ByteStore.Shared.DTO;

public class ProductDto
{
    public int? ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int ProductQuantity { get; set; }
    public IFormFile Image { get; set; }
    public string? ImageStorageUrl { get; set; }
    public string? Description { get; set; } //must be not-nulable but for sake of implementation i will leave nulabel for now
    public double? Rate { get; set; }
    public int? TimesRated { get; set; }

}