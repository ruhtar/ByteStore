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
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
