using AuthenticationService.Shared.DTO;

namespace AuthenticationService.Shared.Validator
{
    public interface IUserValidator
    {
        Task<bool> ValidateUser(CreateUserDto user);
        Task<bool> IsUsernameValid(string username);
        bool IsPasswordValid(string password);
    }
}