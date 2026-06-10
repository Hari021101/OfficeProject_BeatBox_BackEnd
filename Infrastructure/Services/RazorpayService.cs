using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;

namespace Infrastructure.Services;

public class RazorpayService : IRazorpayService
{
    private readonly IConfiguration _config;

    public RazorpayService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<RazorpayOrderResponseDto> CreateOrderAsync(
        RazorpayOrderDto dto)
    {
        var client = new RazorpayClient(
            _config["Razorpay:Key"],
            _config["Razorpay:Secret"]
        );

        Dictionary<string, object> options = new();

        options.Add("amount", dto.Amount * 100);
        options.Add("currency", "INR");
        options.Add("receipt", $"order_{dto.OrderId}");

        Razorpay.Api.Order order = client.Order.Create(options);

        return new RazorpayOrderResponseDto
        {
            RazorpayOrderId = order["id"].ToString(),
            Amount = dto.Amount,
            Currency = "INR"
        };
    }
}