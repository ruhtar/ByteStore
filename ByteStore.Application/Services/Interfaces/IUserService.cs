using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Services.Interfaces;

public interface IUserService
{
    Task<string> AuthenticateUser(User user);
    Task RegisterUser(SignupUserDto user);

    //Task<CreateUserDto> GetUserByUsername(string username);
    Task EditUserAddress(Address address, int userId);
    Task<Address> GetUserAddress(int userId);
    Task<ChangePasswordStatusResponse> ChangePassword(int userId, string password, string repassword);
}