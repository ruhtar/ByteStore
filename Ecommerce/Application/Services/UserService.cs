using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Hasher;
using Ecommerce.Infrastructure.Repository;

namespace Ecommerce.Application.Services
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
                return _tokenService.GenerateToken(userRegistered.User.UserId, userRegistered.Role);
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

        public async Task RegisterUser(UserAggregate user)
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
