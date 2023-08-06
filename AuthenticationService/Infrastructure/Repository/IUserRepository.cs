using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task AddUser(UserAggregate user);
        Task<UserAggregate> GetUserAggregate(UserAggregate user);
        Task<User> GetUser(User user);
    }
}