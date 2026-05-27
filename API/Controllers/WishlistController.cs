using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlist()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var wishlist = await _wishlistService.GetWishlistAsync(userId);
        return Ok(wishlist);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> ToggleWishlistItem(Guid productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _wishlistService.ToggleWishlistItemAsync(userId, productId);
        return Ok(new { Message = "Wishlist updated." });
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearWishlist()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _wishlistService.ClearWishlistAsync(userId);
        return Ok(new { Message = "Wishlist cleared." });
    }
}
