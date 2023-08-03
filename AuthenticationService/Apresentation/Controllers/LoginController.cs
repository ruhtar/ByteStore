using AuthenticationService.Application.Services;
using AuthenticationService.Domain.Entities;
using AuthenticationService.Shared.DTO;
using AuthenticationService.Shared.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                    break;
                case UserValidatorStatus.InvalidPassword:
                    return BadRequest("Your password must have capital letters, numbers and special characters");
                    break;
                case UserValidatorStatus.InvalidRole:
                    return BadRequest("User must be have Admin or User role");
                    break;
                default:
                    break;
            }

            await _userService.AddUser(new User
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            });

            return Ok("User registered.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("signin/admin")]
        public IActionResult SigninAdmin()
        {
            return Ok("Admin, you are signed in! :D");
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("signin/user")]
        public IActionResult SigninUser()
        {
            return Ok("User, you are signed in! :D");
        }
    }
}