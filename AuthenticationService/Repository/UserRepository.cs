
using AuthenticationService.Data;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }
    }
}
