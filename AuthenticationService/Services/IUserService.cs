using AuthenticationService.Models;

namespace AuthenticationService.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<bool> ValidateUser(User user);
    }
}