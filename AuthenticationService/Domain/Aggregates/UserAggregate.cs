using AuthenticationService.Domain.Entities;
using AuthenticationService.Domain.ValueObjects;

namespace AuthenticationService.Domain.Aggregates
{
    public class UserAggregate
    {
        public int UserAggregateId { get; set; }
        public User User { get; set; }
        public Roles Role { get; set; }
        public Address Address { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
