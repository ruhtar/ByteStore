 using AuthenticationService.Domain.Entities;

namespace Ecommerce.Domain.ValueObjects
{
    public class OrderItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

    }
}
