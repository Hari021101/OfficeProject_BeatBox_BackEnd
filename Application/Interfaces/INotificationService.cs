namespace Application.Interfaces;

public interface INotificationService
{
    Task NotifyAdminLowStockAsync(Guid productId, int availableStock);
    Task NotifyNewOrderAsync(int orderId);
    Task BroadcastLiveSalesAsync(object payload);
    Task NotifyOrderStatusAsync(int orderId, string status);
    Task NotifyFlashSaleAsync(object payload);

    Task NotifyDeliveryTrackingAsync(
    int orderId,
    string status,
    string location);

}
