using AuthenticationService.DTO;
using AuthenticationService.Entities;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public LoginController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] UserDTO user)
        {
            var userAuthenticated = await _userService.ValidateUser(new User
            {
                Username = user.Username,
                Password = user.Password
            });
            if (userAuthenticated) 
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(new { token });
            }
            return BadRequest("Username or password are incorrect.");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User is required.");
            }

            var registeredUser = await _userService.GetUserByUsername(user.Username);
            if (registeredUser != null) return BadRequest("User already exists. Please, try other username.");

            await _userService.AddUser(new User
            {
                Username = user.Username,
                Password = user.Password
            });

            return Ok("User registered.");
        }

        [Authorize]
        [HttpGet("/signin")]
        public IActionResult Signin()
        {
            return Ok("You are signed in :)");
        }
    }
}