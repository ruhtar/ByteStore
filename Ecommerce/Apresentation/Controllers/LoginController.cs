using Ecommerce.Application.Services;
using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.Entities;
using Ecommerce.Shared.DTO;
using Ecommerce.Shared.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Host.Controllers
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
        public async Task<IActionResult> Signup([FromBody] RequestUserDto user)
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
                    return BadRequest("User must be have Seller or User role");
                default:
                    break;
            }

            await _userService.RegisterUser(new UserAggregate
            {
                User = new User
                {
                    Username = user.User.Username,
                    Password = user.User.Password,
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
                return Unauthorized("Username or password incorrect.");
            }
            return Ok(new { token });
        }
    }
}