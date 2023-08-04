using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure.Hasher;
using AuthenticationService.Infrastructure.Repository;
using AuthenticationService.Shared.DTO;
using AuthenticationService.Shared.Validator;

namespace AuthenticationService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;


        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> AuthenticateUser(User user)
        {
            var userRegistered = await _userRepository.GetUser(new User
            {
                Username = user.Username
            });

            var isPasswordValid = _passwordHasher.Validate(userRegistered.Password, user.Password);
            if (isPasswordValid) 
            {
                return _tokenService.GenerateToken(user.Username, user.Role);
            }

            return "";
        }

        //public async Task<CreateUserDto> GetUserByUsername(string username)
        //{
        //    var userRegistered = await _userRepository.GetUser(new User
        //    {
        //        Username = username
        //    });
        //    if (userRegistered != null)
        //    {
        //        return new CreateUserDto
        //        {
        //            Username = userRegistered.Username,
        //            Password = userRegistered.Password,
        //        };
        //    }
        //    return null;
        //}

        public async Task AddUser(User user)
        {
            var hashedPassword = _passwordHasher.Hash(user.Password);
            await _userRepository.AddUser(new User
            {
                Username = user.Username,
                Password = hashedPassword,
                Role = user.Role
            });
        }
    }
}
