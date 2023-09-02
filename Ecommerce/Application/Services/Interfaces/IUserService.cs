using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.Entities;
using Ecommerce.Shared.DTO;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> AuthenticateUser(User user);
        Task RegisterUser(UserAggregate user);

        //Task<CreateUserDto> GetUserByUsername(string username);
    }
}