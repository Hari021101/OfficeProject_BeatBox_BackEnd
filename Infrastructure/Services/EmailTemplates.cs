namespace Infrastructure.Services;

public static class EmailTemplates
{
    public static string GetOrderStatusEmail(string customerName, int orderId, string status)
    {
        string statusColor = status switch
        {
            "Pending" => "#f59e0b",
            "Processing" => "#3b82f6",
            "Shipped" => "#8b5cf6",
            "Delivered" => "#22c55e",
            "Cancelled" => "#ef4444",
            _ => "#64748b"
        };

        string statusMessage = status switch
        {
            "Pending" => "Your order has been received and is waiting to be processed.",
            "Processing" => "Our team is preparing your products for shipment.",
            "Shipped" => "Great news! Your order has been shipped and is on its way.",
            "Delivered" => "Your order has been delivered successfully. We hope you enjoy your purchase.",
            "Cancelled" => "Your order has been cancelled. If this was unexpected, please contact support.",
            _ => "Your order status has been updated."
        };

        string emoji = status switch
        {
            "Pending" => "🟡",
            "Processing" => "🔵",
            "Shipped" => "🚚",
            "Delivered" => "✅",
            "Cancelled" => "❌",
            _ => "ℹ️"
        };

        return $@"
<html>
<body style='margin:0;padding:0;background:#f4f6f9;font-family:Segoe UI,Arial,sans-serif;'>
<div style='max-width:700px;margin:30px auto;background:white;border-radius:12px;overflow:hidden;border:1px solid #e5e7eb;'>
    <div style='background:linear-gradient(135deg,#00f3ff,#6c2bff);padding:30px;text-align:center;color:white;'>
        <h1 style='margin:0;font-size:32px;'>🎧 BEATBOX</h1>
        <p style='margin-top:8px;font-size:15px;opacity:0.9;'>Premium Audio Experience</p>
    </div>
    <div style='padding:35px;'>
        <h2 style='margin-top:0;color:#111827;'>Order Update {emoji}</h2>
        <p style='font-size:15px;color:#374151;'>Hello <strong>{customerName}</strong>,</p>
        <p style='font-size:15px;color:#374151;line-height:1.6;'>{statusMessage}</p>
        <div style='background:#f8fafc;padding:25px;border-radius:10px;border-left:5px solid {statusColor};margin:25px 0;'>
            <h3 style='margin-top:0;color:#111827;'>Order Information</h3>
            <p><strong>Order ID:</strong> #{orderId}</p>
            <p><strong>Current Status:</strong></p>
            <div style='margin-top:15px;'>
                <span style='background:{statusColor};color:white;padding:12px 24px;border-radius:25px;font-weight:bold;font-size:15px;'>{status}</span>
            </div>
        </div>
        <div style='background:#f9fafb;padding:20px;border-radius:8px;'>
            <h3 style='margin-top:0;color:#111827;'>Order Journey</h3>
            <p>📦 Order Placed</p>
            <p>⚙️ Processing</p>
            <p>🚚 Shipped</p>
            <p>🏠 Delivered</p>
        </div>
        <p style='margin-top:30px;color:#374151;'>Thank you for choosing BeatBox.</p>
        <p style='color:#6b7280;font-size:14px;'>Need help? Simply reply to this email and our support team will assist you.</p>
    </div>
    <div style='background:#111827;color:white;padding:20px;text-align:center;'>
        <h3 style='margin:0;'>BeatBox Audio</h3>
        <p style='margin-top:10px;color:#d1d5db;font-size:13px;'>Premium Headphones • Speakers • Audio Accessories</p>
        <p style='font-size:12px;color:#9ca3af;margin-top:15px;'>This is an automated email from BeatBox.</p>
    </div>
</div>
</body>
</html>";
    }

    public static string GetOrderConfirmationEmail(string fullName, int orderId, string orderDate, string totalAmount, string shippingAddress)
    {
        return $@"
<html>
<body style='margin:0;padding:0;background:#f4f6f9;font-family:Segoe UI,Arial,sans-serif;'>
<div style='max-width:700px;margin:30px auto;background:white;border-radius:12px;overflow:hidden;border:1px solid #e5e7eb;'>
    <div style='background:linear-gradient(135deg,#00f3ff,#6c2bff);padding:30px;text-align:center;color:white;'>
        <h1 style='margin:0;font-size:32px;'>🎧 BEATBOX</h1>
        <p style='margin-top:8px;font-size:15px;opacity:0.9;'>Premium Audio Experience</p>
    </div>
    <div style='padding:35px;'>
        <h2 style='margin-top:0;color:#111827;'>Order Confirmed 🎉</h2>
        <p style='font-size:15px;color:#374151;'>Hi <strong>{fullName}</strong>,</p>
        <p style='font-size:15px;color:#374151;line-height:1.6;'>Thank you for shopping with BeatBox. We've successfully received your order and it's now being prepared.</p>
        <div style='background:#f8fafc;border-left:5px solid #00f3ff;padding:20px;margin:25px 0;border-radius:8px;'>
            <h3 style='margin-top:0;color:#111827;'>Order Details</h3>
            <p><strong>Order ID:</strong> #{orderId}</p>
            <p><strong>Order Date:</strong> {orderDate}</p>
            <p><strong>Total Amount:</strong> ₹{totalAmount}</p>
            <p><strong>Status:</strong><span style='background:#fff7ed;color:#f59e0b;padding:8px 16px;border-radius:20px;font-weight:bold;margin-left:10px;'>Pending</span></p>
        </div>
        <div style='background:#f9fafb;padding:20px;border-radius:8px;margin-bottom:25px;'>
            <h3 style='margin-top:0;color:#111827;'>Delivery Address</h3>
            <p style='line-height:1.7;color:#374151;'>{shippingAddress}</p>
        </div>
        <div style='background:#eefcff;padding:20px;border-radius:8px;'>
            <h3 style='margin-top:0;color:#111827;'>What happens next?</h3>
            <p>✅ Order received</p>
            <p>📦 Preparing your products</p>
            <p>🚚 Shipping update will be emailed soon</p>
            <p>🎉 Delivery within 3-5 business days</p>
        </div>
        <p style='margin-top:30px;color:#374151;'>Thank you for choosing BeatBox.</p>
    </div>
    <div style='background:#111827;color:white;padding:20px;text-align:center;'>
        <h3 style='margin:0;'>BeatBox Audio</h3>
        <p style='margin-top:10px;color:#d1d5db;font-size:13px;'>Premium Headphones • Speakers • Audio Accessories</p>
        <p style='font-size:12px;color:#9ca3af;margin-top:15px;'>This is an automated email from BeatBox.</p>
    </div>
</div>
</body>
</html>";
    }
}
