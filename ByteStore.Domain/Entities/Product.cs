using ByteStore.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ByteStore.Domain.Entities
{
    public class Product
    {
        [Key]
        [Column("Id")]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageStorageUrl { get; set; }

        [JsonIgnore]
        public ICollection<ShoppingCartProduct>? ShoppingCartProducts { get; set; }
        
    }
}
