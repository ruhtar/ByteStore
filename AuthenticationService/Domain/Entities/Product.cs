using AuthenticationService.Domain.Aggregates;

namespace AuthenticationService.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IList<ShoppingCartProducts> ShoppingCartProducts { get; set; }
    }
}
