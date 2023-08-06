using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Infrastructure;

namespace Ecommerce.Infrastructure.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateShoppingCart(int userAggregateId)
        {
            var shoppingCart = new ShoppingCart
            {
                UserAggregateId = userAggregateId,
                OrderId = null,
                Order = null
            };
            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();
        }

    }
}
