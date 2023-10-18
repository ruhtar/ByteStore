using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(UserAggregate user);
        Task<ChangePasswordStatusResponse> UpdateUserPassword(int userId, string hashedPassword);
        Task<UserAggregate> GetUserAggregate(UserAggregate user);
        Task EditUserAddress(Address address, int userId);
        Task<Address> GetUserAddress(int userId);
    }
}