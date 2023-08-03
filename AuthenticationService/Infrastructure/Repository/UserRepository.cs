using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure;
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

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        }
    }
}
