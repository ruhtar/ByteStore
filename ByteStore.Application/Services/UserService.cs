using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Services
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
            var userRegistered = await _userRepository.GetUserAggregate(new UserAggregate()
            {
                User = new User
                {
                    Username = user.Username
                }
            });
            if (userRegistered == null) return "";

            var isPasswordValid = _passwordHasher.Validate(userRegistered.User.Password, user.Password);
            if (isPasswordValid)
            {
                return _tokenService.GenerateToken(userRegistered.User.UserId, userRegistered.User.Username, userRegistered.Role);
            }

            return "";
        }

        //public async Task<User> GetUserByUsername(string username)
        //{
        //    var userRegistered = await _userRepository.GetUser(new User
        //    {
        //        Username = username
        //    });
        //    if (userRegistered != null)
        //    {
        //        return new User
        //        {
        //            UserId = userRegistered.UserId,
        //            Username = userRegistered.Username,
        //            Password = userRegistered.Password,
        //        };
        //    }
        //    return null;
        //}

        public async Task RegisterUser(SignupUserDto user)
        {
            var hashedPassword = _passwordHasher.Hash(user.User.Password);
            await _userRepository.RegisterUser(new UserAggregate
            {
                User = new User
                {
                    Username = user.User.Username,
                    Password = hashedPassword,
                },
                Address = user.Address,
                Role = user.Role
            });
        }
    }
}
