using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTO
{
    public class CreateUserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
