using AuthenticationService.Domain.Aggregates;
using AuthenticationService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
