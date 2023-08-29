using Ecommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTO
{
    public class CreateUserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Roles Role { get; set; }
        public Address Address { get; set; }
    }
}
