using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Domain.Aggregates
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int UserAggregateId { get; set; }
        public UserAggregate UserAggregate { get; set; }
        public List<CartItem> Items { get; set; }
        public IList<ShoppingCartProducts> ShoppingCartProduct { get; set; }
    }

    public class ShoppingCartProducts 
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
