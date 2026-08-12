namespace Application.DTOs.Admin;

public class DashboardSummaryDto
{
    public decimal TotalRevenue { get; set; }
    public decimal RevenueGrowthPercentage { get; set; }
    public decimal RevenueChangePercentage { get; set; }

    public int TotalOrders { get; set; }
    public decimal OrdersGrowthPercentage { get; set; }
    public decimal OrdersChangePercentage { get; set; }

    public int ActiveUsers { get; set; }
    public int TotalCustomers { get; set; }
    public decimal ActiveUsersChangePercentage { get; set; }
    public decimal CustomerGrowthPercentage { get; set; }

    public decimal ConversionRate { get; set; }
    public decimal ConversionRateChangePercentage { get; set; }

    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public int TotalInventoryItems { get; set; }
    public int PendingOrders { get; set; }
    public int ProcessingOrders { get; set; }
    public int ShippedOrders { get; set; }
    public int DeliveredOrders { get; set; }
    public int CancelledOrders { get; set; }
    public int LowStockProducts { get; set; }
}
