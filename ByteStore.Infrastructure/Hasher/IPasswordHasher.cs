namespace ByteStore.Infrastructure.Hasher;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Validate(string passwordHash, string password);
}