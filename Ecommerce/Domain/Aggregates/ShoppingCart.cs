using Ecommerce.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Aggregates
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
    }
    
    public class ShoppingCartProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }

}
