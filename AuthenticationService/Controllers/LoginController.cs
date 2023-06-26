using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        [HttpGet("user")]
        public string Login()
        {
            return "Deu bom";
        }
    }
}