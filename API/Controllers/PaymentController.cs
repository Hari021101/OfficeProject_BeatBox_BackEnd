using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentProcessDto paymentProcessDto)
    {
        var paymentResponse = await _paymentService.ProcessPaymentAsync(paymentProcessDto);
        return Ok(paymentResponse);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetPaymentByOrderId(int orderId)
    {
        var payment = await _paymentService.GetPaymentByOrderIdAsync(orderId);
        return Ok(payment);
    }
}