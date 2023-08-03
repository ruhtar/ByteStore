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
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] CreateUserDTO user)
        {
            if (user == null)
            {
                return BadRequest("User is required.");
            }
            if(user.Role!="Admin" && user.Role != "User") return BadRequest("Role must be User or Admin.");

            var registeredUser = await _userService.GetUserByUsername(user.Username);
            if (registeredUser != null) return BadRequest("User already exists. Please, try other username.");

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