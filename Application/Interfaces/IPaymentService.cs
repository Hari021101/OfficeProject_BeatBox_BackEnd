using Application.DTOs;

namespace Application.Interfaces;

public interface IPaymentService
{
    Task<PaymentResponseDto> ProcessPaymentAsync(PaymentProcessDto paymentProcessDto);
    Task<PaymentDto> GetPaymentByOrderIdAsync(int orderId);
}