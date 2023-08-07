using AuthenticationService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ecommerce.Domain.ValueObjects;
using System.Text.Json;
using System.Text;

namespace AuthenticationService.Domain.Aggregates
{
    public class ShoppingCart
    {
        [Key]
        [Column("Id")]
        public int ShoppingCartId { get; set; }
        public int UserAggregateId { get; set; }
        public UserAggregate UserAggregate { get; set; }
        public byte[]? OrderItems { get; set; }
        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public decimal TotalValue { get; set; }
    }
    
    public class ShoppingCartProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }

}
