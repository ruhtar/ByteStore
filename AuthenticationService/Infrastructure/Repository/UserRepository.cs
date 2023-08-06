using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUser(UserAggregate userAggregate)
        {
            await _context.UserAggregates.AddAsync(userAggregate);
            await _context.SaveChangesAsync();
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
