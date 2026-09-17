namespace Application.DTOs.Admin;

public class ProductSalesReportDto
{
    public ProductSalesMetricsDto Metrics { get; set; } = new();
    public IEnumerable<ProductSalesOverviewDto> Products { get; set; } = new List<ProductSalesOverviewDto>();
}
