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
        public IActionResult GenerateToken([FromBody] User model)
        {
            if (model.Username != "string" || model.Password != "string")
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(model.Username);
            return Ok(new { token });
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignupAsync([FromBody] User user)
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