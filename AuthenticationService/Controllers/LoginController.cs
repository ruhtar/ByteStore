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

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
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
    }
}