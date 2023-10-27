using ByteStore.Domain.ValueObjects;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Validator;

public interface IUserValidator
{
    Task<UserValidatorStatus> ValidateUser(SignupUserDto user);
    Task<bool> IsUsernameValid(string username);
    bool IsPasswordValid(string password);
    bool IsRoleValid(Roles role);
}