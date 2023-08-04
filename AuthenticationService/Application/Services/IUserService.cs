using AuthenticationService.Domain.Entities;
using AuthenticationService.Shared.DTO;

namespace AuthenticationService.Application.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<string> AuthenticateUser(User user);
        //Task<CreateUserDto> GetUserByUsername(string username);
    }
}