namespace Application.DTOs.Admin;

public class CustomerAnalyticsDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
    public decimal TotalSpent { get; set; }
}
