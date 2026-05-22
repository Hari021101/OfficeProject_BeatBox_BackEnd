using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class OrderHub : Hub
{
    // Clients can call this to join an order-specific group
    public async Task JoinOrderTracking(string orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Order_{orderId}");
    }

    // Clients can call this to leave tracking
    public async Task LeaveOrderTracking(string orderId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Order_{orderId}");
    }
}
