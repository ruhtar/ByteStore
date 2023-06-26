using AuthenticationService.Models;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            if (model.Username != "string" || model.Password != "string")
            {
                return Unauthorized();
            }

            var token = GenerateToken(model.Username);
            return Ok(new { token });
        }



    }
}