using ByteStore.Domain.Entities;

namespace ByteStore.Domain.Aggregates;

public class PurchasedProductDetail
{
    public Product Product { get; set; }
    public DateTime PurchaseDate { get; set; }
}