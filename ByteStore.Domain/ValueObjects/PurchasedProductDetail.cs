using ByteStore.Domain.Entities;

namespace ByteStore.Domain.ValueObjects;

public class PurchasedProductDetail
{
    public Product Product { get; set; }
    public DateTime PurchaseDate { get; set; }
}