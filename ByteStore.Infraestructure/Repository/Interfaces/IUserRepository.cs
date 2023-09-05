using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;

namespace ByteStore.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(UserAggregate user);
        Task<UserAggregate> GetUserAggregate(UserAggregate user);
    }
}