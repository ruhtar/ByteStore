using System.Text;
using System.Text.Json;
using ByteStore.Application.Services.Interfaces;
using ByteStore.Application.Validator;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;
using ByteStore.Infrastructure.Cache;
using ByteStore.Infrastructure.Hasher;
using ByteStore.Infrastructure.Repositories.Interfaces;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;

namespace ByteStore.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IUserValidator _userValidator;
    private readonly ICacheConfigs _cache;
    private const string UserPurchaseHistoryKey = "UserPurchaseHistoryKey";

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService,
        IUserValidator userValidator, ICacheConfigs cache)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _userValidator = userValidator;
        _cache = cache;
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
        if (!isPasswordValid)
        {
            return string.Empty;
        }
        
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));
        
        return _tokenService.GenerateToken(userRegistered.User.UserId, userRegistered.User.Username,
            userRegistered.Role, key);
    }

    public async Task EditUserAddress(Address address, int userId)
    {
        await _userRepository.EditUserAddress(address, userId);
    }

    public async Task<Address?> GetUserAddress(int userId)
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

    public async Task<string?> GetUserPurchaseHistory(int userId)
    {
        return await _userRepository.GetUserPurchaseHistory(userId);
    }

    public async Task<bool> CheckIfUserHasBoughtAProduct(int userId, int productId)
    {
        return await _userRepository.CheckIfUserHasBoughtAProduct(userId, productId);
    }

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