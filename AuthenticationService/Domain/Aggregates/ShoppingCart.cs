using AuthenticationService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Domain.Aggregates
{
    public class ShoppingCart
    {
        [Key]
        [Column("Id")]
        public int ShoppingCartId { get; set; }
        public int UserAggregateId { get; set; }
        public UserAggregate UserAggregate { get; set; }
        public Order Order { get; set; }
        //Todo: Valor total
    }

}
