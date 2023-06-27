using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
