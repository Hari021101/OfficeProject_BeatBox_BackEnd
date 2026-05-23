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

    public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
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