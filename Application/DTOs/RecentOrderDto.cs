public class RecentOrderDto
{
    public int OrderId { get; set; }

    public string UserId { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public int ItemCount { get; set; }
}