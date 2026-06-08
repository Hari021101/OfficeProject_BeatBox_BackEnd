using Application.DTOs.Admin;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAdminDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
    Task<IEnumerable<TimeSeriesDto>> GetSalesAsync(DateTime from, DateTime to, string period);
    Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(int year);
    Task<IEnumerable<ProductAnalyticsDto>> GetTopProductsAsync(int take);
    Task<IEnumerable<ProductAnalyticsDto>> GetWorstProductsAsync(int take);
    Task<IEnumerable<CustomerAnalyticsDto>> GetTopCustomersAsync(int take);
    Task<IEnumerable<ProductAnalyticsDto>> GetLowStockProductsAsync(int take);
    Task<IEnumerable<RecentOrderDto>> GetRecentOrdersAsync(int take);
}
