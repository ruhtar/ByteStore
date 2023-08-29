using Ecommerce.Domain.Aggregates;
using Ecommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    public class User
    {
        [Key]
        [Column("Id")]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
