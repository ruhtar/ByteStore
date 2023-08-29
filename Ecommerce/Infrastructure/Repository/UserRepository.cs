using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public UserRepository(AppDbContext context, IShoppingCartRepository shoppingCartRepository)
        {
            _context = context;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task RegisterUser(UserAggregate userAggregate)
        {
            await _context.UserAggregates.AddAsync(userAggregate);
            await _context.SaveChangesAsync();
            await _shoppingCartRepository.CreateShoppingCart(userAggregate.UserAggregateId);
        }

        public async Task<UserAggregate> GetUserAggregate(UserAggregate user)
        {
            return await _context.UserAggregates.Include(x=>x.User).FirstOrDefaultAsync(u => u.User.Username == user.User.Username);
        }

        public async Task<User> GetUser(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        }
    }
}
