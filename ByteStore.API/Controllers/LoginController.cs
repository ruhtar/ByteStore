using ByteStore.Application.Services.Interfaces;
using ByteStore.Domain.Aggregates;
using ByteStore.Domain.Entities;
using ByteStore.Shared.DTO;
using ByteStore.Shared.Enums;
using ByteStore.Shared.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ByteStore.Host.Controllers
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
        public async Task<IActionResult> Signup([FromBody] SignupUserDto user)
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
            }

            await _userService.RegisterUser(user);

            return Ok("User registered.");
        }

        [HttpPost("signin")]
        public async Task<ActionResult<string>> Signin([FromBody] LoginDto user)
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