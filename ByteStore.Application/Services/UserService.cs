using System.Text.Json;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Repository.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUserValidator _userValidator;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService,
        IUserValidator userValidator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userValidator = userValidator;
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
        if (userRegistered == null) return string.Empty;

        var isPasswordValid = _passwordHasher.Validate(userRegistered.User.Password, user.Password);
        if (isPasswordValid)
            return _tokenService.GenerateToken(userRegistered.User.UserId, userRegistered.User.Username,
                userRegistered.Role);

        return string.Empty;
    }

    public async Task EditUserAddress(Address address, int userId)
    {
        await _userRepository.EditUserAddress(address, userId);
    }

    public async Task<Address> GetUserAddress(int userId)
    {
        return await _userRepository.GetUserAddress(userId);
    }

    public async Task<ChangePasswordStatusResponse> ChangePassword(int userId, string password, string repassword)
    {
        if (!string.Equals(password, repassword)) return ChangePasswordStatusResponse.NotMatching;
        if (!_userValidator.IsPasswordValid(password)) return ChangePasswordStatusResponse.InvalidPassword;

        var hashedPassword = _passwordHasher.Hash(password);
        return await _userRepository.UpdateUserPassword(userId, hashedPassword);
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
                Password = hashedPassword
            },
            Address = user.Address,
            Role = user.Role,
            PurchaseHistory = JsonSerializer.Serialize(new List<PurchasedProductDetail>())
        });
    }
}