using AuthenticationService.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthenticationService.Domain.Entities
{
    public class Product
    {
        [Key]
        [Column("Id")]
        [JsonIgnore]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<ShoppingCartProduct>? ShoppingCartProducts { get; set; }
    }
}
