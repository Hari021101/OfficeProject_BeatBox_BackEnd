namespace Application.DTOs.Admin;

public class ProductSalesOverviewDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int UnitsSold { get; set; }
    public int OrderCount { get; set; }
    public decimal Revenue { get; set; }
    public string? ImageUrl { get; set; }
}
