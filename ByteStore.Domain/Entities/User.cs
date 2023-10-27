using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ByteStore.Domain.Entities;

public class User
{
    [Key] [Column("Id")] public int UserId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}