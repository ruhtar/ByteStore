using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;

namespace ByteStore.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(UserAggregate user);
        Task<UserAggregate> GetUserAggregate(UserAggregate user);
        Task EditUserAddress(Address address, int userId);
        Task<Address> GetUserAddress(int userId);
    }
}