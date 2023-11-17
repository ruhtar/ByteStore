using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Validator;

public class UserValidator : IUserValidator
{
    private readonly IUserRepository _userRepository;

    public UserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserValidatorStatus> ValidateUser(SignupUserDto user)
    {
        if (!IsPasswordValid(user.User.Password)) return UserValidatorStatus.InvalidPassword;

        if (!IsRoleValid(user.Role)) return UserValidatorStatus.InvalidRole;

        if (!await IsUsernameValid(user.User.Username)) return UserValidatorStatus.UsernameAlreadyExists;

        return UserValidatorStatus.Success;
    }

    //TODO: REFATORAR O APPDBCONTEXT PARA QUE USERNAME SEJA UNIQUE INDEX
    public async Task<bool> IsUsernameValid(string username)
    {
        var user = await _userRepository.GetUserAggregate(new UserAggregate
            { User = new User() { Username = username } });
        return user == null;
    }

    public bool IsPasswordValid(string password)
    {
        if (!password.Any(char.IsUpper)) return false;

        if (!password.Any(c => !char.IsLetterOrDigit(c))) return false;

        if (!password.Any(char.IsDigit)) return false;

        if (password.Length < 5) return false;

        return true;
    }

    public bool IsRoleValid(Roles role)
    {
        return role.ToString() == "Seller" || role.ToString() == "User";
    }
}