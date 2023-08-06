using AuthenticationService.Domain.Entities;

namespace Ecommerce.Domain.ValueObjects
{
    public class Item
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

    }
}
