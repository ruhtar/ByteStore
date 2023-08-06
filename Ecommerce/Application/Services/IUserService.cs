using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Shared.DTO;

namespace AuthenticationService.Application.Services
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(User user);
        Task AddUser(UserAggregate user);

        //Task<CreateUserDto> GetUserByUsername(string username);
    }
}