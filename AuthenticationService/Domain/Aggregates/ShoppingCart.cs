using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Domain.Aggregates
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int UserAggregateId { get; set; }
        public UserAggregate UserAggregate { get; set; }
        public Order Order { get; set; }
        //Todo: Valor total
    }

}
