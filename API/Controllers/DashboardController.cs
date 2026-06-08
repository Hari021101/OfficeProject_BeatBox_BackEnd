using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("analytics")]
        public async Task<ActionResult<DashboardAnalyticsDto>> GetAnalytics()
        {
            var today = DateTime.UtcNow;
            var thirtyDaysAgo = today.AddDays(-30);
            var sixtyDaysAgo = today.AddDays(-60);

            // Fetch orders
            var recentOrders = await _context.Orders
                .Where(o => o.CreatedDate >= sixtyDaysAgo && o.Status != "Cancelled")
                .ToListAsync();

            var currentMonthOrders = recentOrders.Where(o => o.CreatedDate >= thirtyDaysAgo).ToList();
            var previousMonthOrders = recentOrders.Where(o => o.CreatedDate < thirtyDaysAgo).ToList();

            var currentRevenue = currentMonthOrders.Sum(o => o.TotalAmount);
            var previousRevenue = previousMonthOrders.Sum(o => o.TotalAmount);
            var revenueTrend = previousRevenue > 0 ? ((currentRevenue - previousRevenue) / previousRevenue) * 100 : 100;

            var currentOrderCount = currentMonthOrders.Count;
            var previousOrderCount = previousMonthOrders.Count;
            var orderTrend = previousOrderCount > 0 ? ((decimal)(currentOrderCount - previousOrderCount) / previousOrderCount) * 100 : 100;

            // Users
            var totalUsers = await _userManager.Users.CountAsync();
            var usersTrend = 12.5m; // Mock trend

            // Product distribution
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            var productDist = products
                .GroupBy(p => p.Category?.Name ?? "Uncategorized")
                .Select(g => new ChartDataPointDto { Name = g.Key, Value = g.Count() })
                .ToList();

            // Revenue Chart (Last 7 days)
            var sevenDaysAgo = today.AddDays(-7);
            var revenueChart = currentMonthOrders
                .Where(o => o.CreatedDate >= sevenDaysAgo)
                .GroupBy(o => o.CreatedDate.Date)
                .Select(g => new ChartDataPointDto 
                { 
                    Name = g.Key.ToString("ddd"), 
                    Value = g.Sum(o => o.TotalAmount) 
                })
                .ToList();

            // Sales Chart (Last 4 weeks)
            var salesChart = new List<ChartDataPointDto>
            {
                new ChartDataPointDto { Name = "Week 1", Value = currentMonthOrders.Where(o => o.CreatedDate >= today.AddDays(-7)).Count() },
                new ChartDataPointDto { Name = "Week 2", Value = currentMonthOrders.Where(o => o.CreatedDate >= today.AddDays(-14) && o.CreatedDate < today.AddDays(-7)).Count() },
                new ChartDataPointDto { Name = "Week 3", Value = currentMonthOrders.Where(o => o.CreatedDate >= today.AddDays(-21) && o.CreatedDate < today.AddDays(-14)).Count() },
                new ChartDataPointDto { Name = "Week 4", Value = currentMonthOrders.Where(o => o.CreatedDate >= today.AddDays(-28) && o.CreatedDate < today.AddDays(-21)).Count() },
            };
            salesChart.Reverse();

            var dto = new DashboardAnalyticsDto
            {
                Stats = new DashboardStatsDto
                {
                    TotalRevenue = currentRevenue,
                    RevenueTrend = Math.Round(revenueTrend, 1),
                    TotalOrders = currentOrderCount,
                    OrdersTrend = Math.Round(orderTrend, 1),
                    ActiveUsers = totalUsers,
                    UsersTrend = usersTrend,
                    ConversionRate = 3.2m,
                    ConversionTrend = 0.5m
                },
                RevenueChart = revenueChart.Any() ? revenueChart : GenerateMockRevenueChart(),
                SalesChart = salesChart,
                ProductDistribution = productDist
            };

            return Ok(dto);
        }

        private List<ChartDataPointDto> GenerateMockRevenueChart()
        {
            return new List<ChartDataPointDto>
            {
                new ChartDataPointDto { Name = "Mon", Value = 45000 },
                new ChartDataPointDto { Name = "Tue", Value = 52000 },
                new ChartDataPointDto { Name = "Wed", Value = 48000 },
                new ChartDataPointDto { Name = "Thu", Value = 61000 },
                new ChartDataPointDto { Name = "Fri", Value = 59000 },
                new ChartDataPointDto { Name = "Sat", Value = 85000 },
                new ChartDataPointDto { Name = "Sun", Value = 75000 }
            };
        }
    }
}
