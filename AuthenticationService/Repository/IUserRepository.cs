using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public interface IUserRepository
    {
        void AddUser(User user);
    }
}