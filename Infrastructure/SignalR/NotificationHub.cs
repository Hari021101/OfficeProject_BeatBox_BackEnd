using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

[Authorize]
public class NotificationHub : Hub
{
    // Call from client after connecting to register as Admin. We avoid overriding lifecycle methods to remain friendly to hot-reload.
    public async Task RegisterAsAdmin()
    {
        var user = Context.User;
        if (user != null && user.IsInRole("Admin"))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");

        }

    }
    public async Task JoinAdminDashboard()
    {
        if (Context.User!.IsInRole("Admin"))
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                "DashboardAdmins");
        }
    }

    public async Task UnregisterAsAdmin()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
    }
}
