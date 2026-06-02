using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

[Authorize]
public class OrderTrackingHub : Hub
{
    // Clients call this to subscribe to updates for a specific order
    public async Task JoinOrderGroup(string orderId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, orderId);
    }

    public async Task LeaveOrderGroup(string orderId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId);
    }

    // Clients can call LeaveOrderGroup when they want to stop receiving updates for an order.
}
