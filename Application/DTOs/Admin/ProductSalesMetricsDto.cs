namespace Application.DTOs.Admin;

public class ProductSalesMetricsDto
{
    public decimal CurrentMonthRevenue { get; set; }
    public decimal PreviousMonthRevenue { get; set; }
    public decimal RevenueChangePercent { get; set; }

    public int CurrentMonthUnitsSold { get; set; }
    public int PreviousMonthUnitsSold { get; set; }
    public decimal UnitsChangePercent { get; set; }

    public int CurrentMonthOrders { get; set; }
    public int PreviousMonthOrders { get; set; }
    public decimal OrdersChangePercent { get; set; }

    public int CurrentMonthActiveProducts { get; set; }
    public int PreviousMonthActiveProducts { get; set; }
    public decimal ActiveProductsChangePercent { get; set; }

    public int TotalProductsCount { get; set; }
}
