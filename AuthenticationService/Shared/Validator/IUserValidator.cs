using AuthenticationService.Domain.Enums;
using AuthenticationService.Shared.DTO;

namespace AuthenticationService.Shared.Validator
{
    public interface IUserValidator
    {
        Task<UserValidatorStatus> ValidateUser(CreateUserDto user);
        Task<bool> IsUsernameValid(string username);
        bool IsPasswordValid(string password);
        bool IsRoleValid(Roles role);
    }
}