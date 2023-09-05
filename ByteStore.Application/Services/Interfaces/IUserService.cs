using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;

namespace ByteStore.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(User user);
        Task RegisterUser(SignupUserDto user);

        //Task<CreateUserDto> GetUserByUsername(string username);
    }
}