namespace Application.DTOs;

public class DashboardStatsDto
{
    public decimal TotalRevenue { get; set; }
    public decimal RevenueTrend { get; set; } // Percentage change vs last month
    public int TotalOrders { get; set; }
    public decimal OrdersTrend { get; set; }
    public int ActiveUsers { get; set; }
    public decimal UsersTrend { get; set; }
    public decimal ConversionRate { get; set; }
    public decimal ConversionTrend { get; set; }
}

public class ChartDataPointDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}

public class DashboardAnalyticsDto
{
    public DashboardStatsDto Stats { get; set; } = new DashboardStatsDto();
    public List<ChartDataPointDto> RevenueChart { get; set; } = new List<ChartDataPointDto>();
    public List<ChartDataPointDto> SalesChart { get; set; } = new List<ChartDataPointDto>();
    public List<ChartDataPointDto> ProductDistribution { get; set; } = new List<ChartDataPointDto>();
}
