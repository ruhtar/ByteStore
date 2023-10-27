using ByteStore.Domain.Entities;
using ByteStore.Domain.ValueObjects;

namespace ByteStore.Shared.DTO;

public class SignupUserDto
{
    public User User { get; set; }
    public Roles Role { get; set; }
    public Address Address { get; set; }
}