using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;

namespace ByteStore.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(User user);
        Task RegisterUser(SignupUserDto user);

        //Task<CreateUserDto> GetUserByUsername(string username);
        Task EditUserAddress(Address address, int userId);
        Task<Address> GetUserAddress(int userId);
    }
}