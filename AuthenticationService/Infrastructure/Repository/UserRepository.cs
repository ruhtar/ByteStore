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
            //TODO: UNIT OF WORK
            var user = new User
            {
                Username = userAggregate.User.Username,
                Password= userAggregate.User.Password,
            };
            await _context.Users.AddAsync(user);
            await _context.UserAggregates.AddAsync(userAggregate);
            await _context.SaveChangesAsync();
        }

        public async Task<UserAggregate> GetUser(UserAggregate user)
        {
            return await _context.UserAggregates.FirstOrDefaultAsync(u => u.User.Username == user.User.Username);
        }
    }
}
