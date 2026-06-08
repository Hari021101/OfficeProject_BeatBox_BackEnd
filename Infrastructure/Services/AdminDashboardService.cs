using Application.DTOs.Admin;
using Application.Interfaces;

namespace Infrastructure.Services;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly IAdminDashboardRepository _repo;
    private readonly INotificationService _notifier;

    public AdminDashboardService(IAdminDashboardRepository repo, INotificationService notifier)
    {
        _repo = repo;
        _notifier = notifier;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var summary = await _repo.GetSummaryAsync();
        return summary;
    }

    public async Task<IEnumerable<TimeSeriesDto>> GetSalesAsync(DateTime from, DateTime to, string period)
    {
        return await _repo.GetSalesTimeseriesAsync(from, to, period);
    }

    public async Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(int year)
    {
        return await _repo.GetRevenueByMonthAsync(year);
    }

    public async Task<IEnumerable<ProductAnalyticsDto>> GetTopProductsAsync(int take)
    {
        return await _repo.GetTopProductsAsync(take);
    }

    public async Task<IEnumerable<ProductAnalyticsDto>> GetWorstProductsAsync(int take)
    {
        return await _repo.GetWorstProductsAsync(take);
    }

    public async Task<IEnumerable<CustomerAnalyticsDto>> GetTopCustomersAsync(int take)
    {
        return await _repo.GetTopCustomersBySpendingAsync(take);
    }

    public async Task<IEnumerable<ProductAnalyticsDto>> GetLowStockProductsAsync(int take)
    {
        return await _repo.GetLowStockProductsAsync(take);
    }

    public async Task<IEnumerable<RecentOrderDto>> GetRecentOrdersAsync(int take)
    {
        return await _repo.GetRecentOrdersAsync(take);
    }
}
