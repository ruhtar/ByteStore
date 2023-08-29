using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.ValueObjects;
using Ecommerce.Infrastructure.Repository;
using Ecommerce.Shared.DTO;

namespace Ecommerce.Shared.Validator
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserValidatorStatus> ValidateUser(CreateUserDto user)
        {
            var isPasswordValid = IsPasswordValid(user.Password);
            if (!isPasswordValid) return UserValidatorStatus.InvalidPassword;

            var isRoleValid = IsRoleValid(user.Role);
            if (!isRoleValid) return UserValidatorStatus.InvalidRole;

            var isUsernameValid = await IsUsernameValid(user.Username);
            if (!isUsernameValid) return UserValidatorStatus.UsernameAlreadyExists;

            return UserValidatorStatus.Success;
        }

        public async Task<bool> IsUsernameValid(string username)
        {
            var user = await _userRepository.GetUserAggregate(new UserAggregate { User = new User() { Username = username } });
            return user == null;
        }

        public bool IsPasswordValid(string password)
        {
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            if (password.Length < 5)
            {
                return false;
            }

            return true;
        }

        public bool IsRoleValid(Roles role)
        {
            return role.ToString() == "Admin" || role.ToString() == "User";
        }
    }
}
