using Ecommerce.Domain.ValueObjects;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Enums;

namespace Ecommerce.Shared.Validator
{
    public interface IUserValidator
    {
        Task<UserValidatorStatus> ValidateUser(RequestUserDto user);
        Task<bool> IsUsernameValid(string username);
        bool IsPasswordValid(string password);
        bool IsRoleValid(Roles role);
    }
}