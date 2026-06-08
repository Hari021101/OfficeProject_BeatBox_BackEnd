using Application.DTOs.Admin;

namespace Application.Interfaces;

public interface IAdminDashboardRepository
{
    Task<DashboardSummaryDto> GetSummaryAsync();
    Task<IEnumerable<TimeSeriesDto>> GetSalesTimeseriesAsync(DateTime from, DateTime to, string period); // period: daily, weekly, monthly
    Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(int year);
    Task<IEnumerable<ProductAnalyticsDto>> GetTopProductsAsync(int take);
    Task<IEnumerable<ProductAnalyticsDto>> GetWorstProductsAsync(int take);
    Task<IEnumerable<ProductAnalyticsDto>> GetLowStockProductsAsync(int take);
    Task<IEnumerable<CustomerAnalyticsDto>> GetTopCustomersByOrdersAsync(int take);
    Task<IEnumerable<CustomerAnalyticsDto>> GetTopCustomersBySpendingAsync(int take);
    Task<IEnumerable<RecentOrderDto>> GetRecentOrdersAsync(int take);
}
