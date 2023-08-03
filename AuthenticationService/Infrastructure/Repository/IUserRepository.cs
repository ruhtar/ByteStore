using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUser(User user);
    }
}