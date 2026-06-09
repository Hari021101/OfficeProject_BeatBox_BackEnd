using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IInventoryService _inventoryService;
    private readonly INotificationService _notifier;
    private readonly IAdminDashboardService _dashboardService;

    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IMapper mapper, IInventoryService inventoryService, INotificationService notifier)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
        _inventoryService = inventoryService;
        _notifier = notifier;
    }
    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IMapper mapper, IInventoryService inventoryService, INotificationService notifier, IAdminDashboardService dashboardService)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
        _inventoryService = inventoryService;
        _notifier = notifier;
        _dashboardService = dashboardService;
    }

    public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentProcessDto paymentProcessDto)
    {
        var order = await _orderRepository.GetOrderByIdAsync(paymentProcessDto.OrderId);
        if (order == null) throw new Exception("Order not found");

        if (order.TotalAmount != paymentProcessDto.Amount)
            throw new Exception("Payment amount mismatch");

        var payment = new Payment
        {
            OrderId = paymentProcessDto.OrderId,
            Amount = paymentProcessDto.Amount,
            Method = paymentProcessDto.Method,
            Status = "Success",
            TransactionId = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.UtcNow
        };

        await _paymentRepository.AddPaymentAsync(payment);
        await _paymentRepository.SaveChangesAsync();

        order.Status = "Processing";
        await _orderRepository.SaveChangesAsync();

        // Finalize inventory reservations for order items
        foreach (var item in order.OrderItems)
        {
            await _inventoryService.FinalizeReservationAsync(item.ProductId, item.Quantity, "system");
        }

        // Notify order status update to customers via SignalR
        await _notifier.NotifyOrderStatusAsync(order.OrderId, order.Status);
        await _notifier.BroadcastLiveSalesAsync(new
        {
            OrderId = order.OrderId,
            Amount = order.TotalAmount,
            Timestamp = DateTime.UtcNow
        });

        // Broadcast updated dashboard summary (best-effort)
        try
        {
            var summary = await _dashboardService.GetSummaryAsync();
            await _notifier.NotifyDashboardUpdatedAsync(summary);
        }
        catch
        {
        }
        return new PaymentResponseDto
        {
            Status = payment.Status,
            TransactionId = payment.TransactionId,
            Message = "Payment processed successfully"
        };
    }

    public async Task<PaymentDto> GetPaymentByOrderIdAsync(int orderId)
    {
        var payment = await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
        return _mapper.Map<PaymentDto>(payment);
    }
}