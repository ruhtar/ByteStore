using AuthenticationService.Authentication;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;


        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> ValidateUser(User user)
        {
            var hashedPassword = _passwordHasher.Hash(user.Password);
            user.Password = hashedPassword;
            var userAuthenticated = await _userRepository.GetUser(user);
            if (userAuthenticated!=null) return true;
            return false;
        }



        public async Task AddUser(User user)
        {
            var hashedPassword = _passwordHasher.Hash(user.Password);
            user.Password = hashedPassword;
            await _userRepository.AddUser(user);
        }

    }
}
