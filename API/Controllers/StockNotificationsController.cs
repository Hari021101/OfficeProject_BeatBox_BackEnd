using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockNotificationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StockNotificationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("subscribe")]
    [Authorize]
    public async Task<IActionResult> Subscribe([FromBody] StockNotificationRequestDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var variant = await _context.ProductVariants
            .Include(v => v.Product)
            .FirstOrDefaultAsync(v => v.Id == dto.VariantId && v.ProductId == dto.ProductId);

        if (variant == null)
        {
            return NotFound(new { message = "Product variant not found." });
        }

        // Check if already active
        var existing = await _context.StockNotificationSubscriptions
            .FirstOrDefaultAsync(s => s.UserId == userId && s.ProductVariantId == dto.VariantId && s.IsActive);

        if (existing != null)
        {
            return Ok(new { success = true, message = "You will be notified when this product is back in stock.", isSubscribed = true });
        }

        var subscription = new StockNotificationSubscription
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ProductId = dto.ProductId,
            ProductVariantId = dto.VariantId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.StockNotificationSubscriptions.Add(subscription);
        await _context.SaveChangesAsync();

        return Ok(new { success = true, message = "You will be notified when this product is back in stock.", isSubscribed = true });
    }

    [HttpDelete("unsubscribe/{variantId}")]
    [Authorize]
    public async Task<IActionResult> Unsubscribe(Guid variantId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var existing = await _context.StockNotificationSubscriptions
            .FirstOrDefaultAsync(s => s.UserId == userId && s.ProductVariantId == variantId && s.IsActive);

        if (existing != null)
        {
            existing.IsActive = false;
            await _context.SaveChangesAsync();
        }

        return Ok(new { success = true, message = "Unsubscribed successfully.", isSubscribed = false });
    }

    [HttpGet("status/{variantId}")]
    public async Task<IActionResult> GetStatus(Guid variantId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Ok(new { isSubscribed = false });
        }

        var isSubscribed = await _context.StockNotificationSubscriptions
            .AnyAsync(s => s.UserId == userId && s.ProductVariantId == variantId && s.IsActive);

        return Ok(new { isSubscribed });
    }
}
