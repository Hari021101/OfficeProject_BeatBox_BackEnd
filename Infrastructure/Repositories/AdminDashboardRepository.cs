using Application.DTOs.Admin;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AdminDashboardRepository : IAdminDashboardRepository
{
    private readonly AppDbContext _context;

    public AdminDashboardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        // Use aggregation queries with AsNoTracking
        var totalRevenue = await _context.Payments.AsNoTracking().SumAsync(p => (decimal?)p.Amount) ?? 0m;
        var totalOrders = await _context.Orders.AsNoTracking().CountAsync();
        var totalCustomers = await _context.Users.AsNoTracking().CountAsync();
        var totalProducts = await _context.Products.AsNoTracking().CountAsync();
        var totalCategories = await _context.Categories.AsNoTracking().CountAsync();
        var totalInventoryItems = await _context.Inventories.AsNoTracking().CountAsync();
        var pending = await _context.Orders.AsNoTracking().CountAsync(o => o.Status == "Pending");
        var processing = await _context.Orders.AsNoTracking().CountAsync(o => o.Status == "Processing");
        var shipped = await _context.Orders.AsNoTracking().CountAsync(o => o.Status == "Shipped");
        var delivered = await _context.Orders.AsNoTracking().CountAsync(o => o.Status == "Delivered");
        var cancelled = await _context.Orders.AsNoTracking().CountAsync(o => o.Status == "Cancelled");
        var lowStock = await _context.Inventories.AsNoTracking().CountAsync(i => i.AvailableStock < i.LowStockThreshold);

        return new DashboardSummaryDto
        {
            TotalRevenue = totalRevenue,
            TotalOrders = totalOrders,
            TotalCustomers = totalCustomers,
            TotalProducts = totalProducts,
            TotalCategories = totalCategories,
            TotalInventoryItems = totalInventoryItems,
            PendingOrders = pending,
            ProcessingOrders = processing,
            ShippedOrders = shipped,
            DeliveredOrders = delivered,
            CancelledOrders = cancelled,
            LowStockProducts = lowStock
        };
    }

    public async Task<IEnumerable<TimeSeriesDto>> GetSalesTimeseriesAsync(DateTime from, DateTime to, string period)
    {
        // Group by day / week / month depending on period
        var query = _context.Orders
            .AsNoTracking()
            .Where(o => o.CreatedDate >= from && o.CreatedDate <= to && o.Status != "Cancelled")
            .Select(o => new { o.CreatedDate, o.TotalAmount });

        var grouped = await query.ToListAsync();

        var result = new List<TimeSeriesDto>();

        if (period == "daily")
        {
            var byDay = grouped.GroupBy(x => x.CreatedDate.Date)
                .Select(g => new TimeSeriesDto { Date = g.Key, Revenue = g.Sum(x => x.TotalAmount), OrdersCount = g.Count() })
                .OrderBy(x => x.Date)
                .ToList();

            return byDay;
        }

        if (period == "monthly")
        {
            var byMonth = grouped.GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
                .Select(g => new TimeSeriesDto { Date = new DateTime(g.Key.Year, g.Key.Month, 1), Revenue = g.Sum(x => x.TotalAmount), OrdersCount = g.Count() })
                .OrderBy(x => x.Date)
                .ToList();

            return byMonth;
        }

        // default weekly
        var byWeek = grouped.GroupBy(x => System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(x.CreatedDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday))
            .Select(g => new TimeSeriesDto { Date = g.Min(x => x.CreatedDate), Revenue = g.Sum(x => x.TotalAmount), OrdersCount = g.Count() })
            .OrderBy(x => x.Date)
            .ToList();

        return byWeek;
    }

    public async Task<IEnumerable<RevenueByMonthDto>> GetRevenueByMonthAsync(int year)
    {
        var items = await _context.Orders.AsNoTracking()
            .Where(o => o.CreatedDate.Year == year && o.Status != "Cancelled")
            .GroupBy(o => new { o.CreatedDate.Year, o.CreatedDate.Month })
            .Select(g => new RevenueByMonthDto { Year = g.Key.Year, Month = g.Key.Month, Revenue = g.Sum(x => x.TotalAmount) })
            .OrderBy(x => x.Month)
            .ToListAsync();

        return items;
    }

    public async Task<IEnumerable<ProductAnalyticsDto>> GetTopProductsAsync(int take)
    {
        var items = await _context.OrderItems.AsNoTracking()
            .GroupBy(oi => oi.ProductId)
            .Select(g => new { ProductId = g.Key, UnitsSold = g.Sum(x => x.Quantity), Revenue = g.Sum(x => x.Quantity * x.UnitPrice) })
            .OrderByDescending(x => x.UnitsSold)
            .Take(take)
            .ToListAsync();

        var result = new List<ProductAnalyticsDto>();
        var productIds = items.Select(i => i.ProductId).ToList();
        var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToDictionaryAsync(p => p.Id, p => p.Name);

        foreach (var it in items)
        {
            products.TryGetValue(it.ProductId, out var name);
            result.Add(new ProductAnalyticsDto { ProductId = it.ProductId, ProductName = name ?? string.Empty, UnitsSold = it.UnitsSold, Revenue = it.Revenue });
        }

        return result;
    }

    public async Task<IEnumerable<ProductAnalyticsDto>> GetWorstProductsAsync(int take)
    {
        var items = await _context.OrderItems.AsNoTracking()
            .GroupBy(oi => oi.ProductId)
            .Select(g => new { ProductId = g.Key, UnitsSold = g.Sum(x => x.Quantity), Revenue = g.Sum(x => x.Quantity * x.UnitPrice) })
            .OrderBy(x => x.UnitsSold)
            .Take(take)
            .ToListAsync();

        var result = new List<ProductAnalyticsDto>();
        var productIds = items.Select(i => i.ProductId).ToList();
        var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToDictionaryAsync(p => p.Id, p => p.Name);

        foreach (var it in items)
        {
            products.TryGetValue(it.ProductId, out var name);
            result.Add(new ProductAnalyticsDto { ProductId = it.ProductId, ProductName = name ?? string.Empty, UnitsSold = it.UnitsSold, Revenue = it.Revenue });
        }

        return result;
    }

    public async Task<IEnumerable<ProductAnalyticsDto>> GetLowStockProductsAsync(int take)
    {
        var items = await _context.Inventories.AsNoTracking()
            .Where(i => i.AvailableStock < i.LowStockThreshold)
            .OrderBy(i => i.AvailableStock)
            .Take(take)
            .Include(i => i.Product)
            .ToListAsync();

        return items.Select(i => new ProductAnalyticsDto
        {
            ProductId = i.ProductId,
            ProductName = i.Product?.Name ?? string.Empty,
            UnitsSold = 0,
            Revenue = 0
        });
    }

    public async Task<IEnumerable<CustomerAnalyticsDto>> GetTopCustomersByOrdersAsync(int take)
    {
        var items = await _context.Orders.AsNoTracking()
            .GroupBy(o => o.UserId)
            .Select(g => new { UserId = g.Key, OrdersCount = g.Count(), Total = g.Sum(x => x.TotalAmount) })
            .OrderByDescending(x => x.OrdersCount)
            .Take(take)
            .ToListAsync();

        var result = new List<CustomerAnalyticsDto>();
        var userIds = items.Select(i => i.UserId).ToList();
        var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToDictionaryAsync(u => u.Id, u => u.Email);

        foreach (var it in items)
        {
            users.TryGetValue(it.UserId, out var email);
            result.Add(new CustomerAnalyticsDto { UserId = it.UserId, Email = email ?? string.Empty, OrdersCount = it.OrdersCount, TotalSpent = it.Total });
        }

        return result;
    }

    public async Task<IEnumerable<CustomerAnalyticsDto>> GetTopCustomersBySpendingAsync(int take)
    {
        var items = await _context.Orders.AsNoTracking()
            .GroupBy(o => o.UserId)
            .Select(g => new { UserId = g.Key, OrdersCount = g.Count(), Total = g.Sum(x => x.TotalAmount) })
            .OrderByDescending(x => x.Total)
            .Take(take)
            .ToListAsync();

        var result = new List<CustomerAnalyticsDto>();
        var userIds = items.Select(i => i.UserId).ToList();
        var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToDictionaryAsync(u => u.Id, u => u.Email);

        foreach (var it in items)
        {
            users.TryGetValue(it.UserId, out var email);
            result.Add(new CustomerAnalyticsDto { UserId = it.UserId, Email = email ?? string.Empty, OrdersCount = it.OrdersCount, TotalSpent = it.Total });
        }

        return result;
    }

    public async Task<IEnumerable<RecentOrderDto>> GetRecentOrdersAsync(int take)
    {
        return await _context.Orders
            .AsNoTracking()
            .OrderByDescending(o => o.CreatedDate)
            .Take(take)
            .Select(o => new RecentOrderDto
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                CreatedDate = o.CreatedDate,
                ItemCount = o.OrderItems.Count
            })
            .ToListAsync();
    }
}
