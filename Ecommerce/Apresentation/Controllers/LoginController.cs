using AuthenticationService.Application.Services;
using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Shared.DTO;
using AuthenticationService.Shared.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace AuthenticationService.Host.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidator _userValidator;

        public LoginController(IUserService userService, IUserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] CreateUserDto user)
        {
            if (user == null)
            {
                return BadRequest("User is required.");
            }

            var userValidatorStatus = await _userValidator.ValidateUser(user);
            switch (userValidatorStatus)
            {
                case UserValidatorStatus.Success:
                    break;
                case UserValidatorStatus.UsernameAlreadyExists:
                    return BadRequest("User already exists. Please, try other username.");
                case UserValidatorStatus.InvalidPassword:
                    return BadRequest("Your password must have capital letters, numbers and special characters");
                case UserValidatorStatus.InvalidRole:
                    return BadRequest("User must be have Admin or User role");
                default:
                    break;
            }

            await _userService.RegisterUser(new UserAggregate
            {
                User = new User
                {
                    Username = user.Username,
                    Password = user.Password,
                },
                Address= user.Address,
                Role = user.Role
            });

            return Ok("User registered.");
        }

        [HttpPost("signin")]
        public async Task<ActionResult<string>> Signin([FromBody] LoginUserDto user)
        {
            var token = await _userService.AuthenticateUser(
                new User
                {
                    Username = user.Username,
                    Password = user.Password
                });
            if (string.IsNullOrEmpty(token))
            {
                return Problem("Username or password incorrect.");
            }
            return Ok(token);
        }
    }
}