using Application.DTOs;

public interface IRazorpayService
{
    Task<RazorpayOrderResponseDto> CreateOrderAsync(
        RazorpayOrderDto dto
    );
}