namespace ByteStore.Shared.DTO;

public class ReviewDto
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string ReviewText { get; set; }
    public double Rate { get; set; }
}