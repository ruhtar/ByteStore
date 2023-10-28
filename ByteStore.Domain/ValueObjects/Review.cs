namespace ByteStore.Domain.ValueObjects;

public class Review
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string ReviewText { get; set; }
}