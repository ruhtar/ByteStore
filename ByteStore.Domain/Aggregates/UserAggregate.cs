using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ByteStore.Domain.Aggregates;

public class UserAggregate
{
    [Key] [Column("Id")] public int UserAggregateId { get; set; }
    public User User { get; set; }
    public Roles Role { get; set; }
    public Address Address { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
    public string PurchaseHistory { get; set; }
    
    public List<PurchasedProductDetail> GetPurchaseHistory()
    {
        return string.IsNullOrEmpty(PurchaseHistory)
            ? new List<PurchasedProductDetail>()
            : JsonSerializer.Deserialize<List<PurchasedProductDetail>>(PurchaseHistory);
    }

    public void AddToPurchaseHistory(IEnumerable<PurchasedProductDetail> purchasedProducts)
    {
        var existingPurchaseHistory = GetPurchaseHistory();
        existingPurchaseHistory.AddRange(purchasedProducts);
        var json = JsonSerializer.Serialize(existingPurchaseHistory);
        PurchaseHistory = json;
    }
}