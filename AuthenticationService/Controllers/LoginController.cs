using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<IActionResult> GenerateTokenAsync([FromBody] User user)
        {
            if (user.Username != "string" || user.Password != "string")
            {
                return Unauthorized();
            }
            var userAuthenticated = await _userService.ValidateUser(user);
            if (userAuthenticated) 
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(new { token });

            }
            return BadRequest("Usuário ou senha incorretos.");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Por favor, insira um usuário.");
            }

            await _userService.AddUser(user);

            return Ok();
        }
    }
}