using AuthenticationService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Shared.DTO
{
    public class CreateUserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }
    }
}
