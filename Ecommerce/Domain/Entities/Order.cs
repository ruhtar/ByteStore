using AuthenticationService.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthenticationService.Domain.Entities
{
    public class Order
    {
        [Key]
        [Column("Id")]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        [JsonIgnore]
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }

    public class OrderProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
