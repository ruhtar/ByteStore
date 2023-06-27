using AuthenticationService.Entities;

namespace AuthenticationService.Repository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUser(User user);
    }
}