using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Infrastructure.SignalR
{
    public class InventoryHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(System.Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        // Clients will listen to "ReceiveStockAlert"
    }
}
