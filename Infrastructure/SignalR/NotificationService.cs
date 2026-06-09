using Application.DTOs.Admin;
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _notificationHub;
    private readonly IHubContext<OrderTrackingHub> _orderHub;

    public NotificationService(IHubContext<NotificationHub> notificationHub, IHubContext<OrderTrackingHub> orderHub)
    {
        _notificationHub = notificationHub;
        _orderHub = orderHub;
    }

    public async Task NotifyAdminLowStockAsync(Guid productId, int availableStock)
    {
        await _notificationHub.Clients.Group("Admins").SendAsync("LowStockAlert", new { ProductId = productId, AvailableStock = availableStock });
    }

    public async Task NotifyNewOrderAsync(int orderId)
    {
        await _notificationHub.Clients.Group("Admins").SendAsync("NewOrderPlaced", new { OrderId = orderId });
    }

    public async Task BroadcastLiveSalesAsync(object payload)
    {
        await _notificationHub.Clients.All.SendAsync("LiveSalesUpdate", payload);
    }

    public async Task NotifyOrderStatusAsync(int orderId, string status)
    {
        await _orderHub.Clients.Group(orderId.ToString()).SendAsync("OrderStatusUpdated", new { OrderId = orderId, Status = status });
    }

    public async Task NotifyFlashSaleAsync(object payload)
    {
        await _notificationHub
            .Clients
            .All
            .SendAsync("FlashSaleStarted", payload);
    }

    public async Task NotifyDeliveryTrackingAsync(
    int orderId,
    string status,
    string location)
    {
        await _orderHub.Clients
            .Group(orderId.ToString())
            .SendAsync(
                "DeliveryTrackingUpdated",
                new
                {
                    OrderId = orderId,
                    Status = status,
                    Location = location
                });
    }

    public async Task NotifyDashboardUpdatedAsync(
    DashboardSummaryDto summary)
    {
        await _notificationHub
            .Clients
            .Group("Admins")
            .SendAsync("DashboardUpdated", summary);
    }


}
