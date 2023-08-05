using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task AddUser(UserAggregate user);
        Task<UserAggregate> GetUser(UserAggregate user);
    }
}