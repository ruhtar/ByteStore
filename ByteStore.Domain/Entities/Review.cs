namespace ByteStore.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    // public Product Product { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public double Rate { get; set; }
    public string ReviewText { get; set; }
}