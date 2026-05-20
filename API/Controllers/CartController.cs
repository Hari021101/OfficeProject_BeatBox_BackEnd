using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = "test-user";
        var cart = await _cartService.GetCartAsync(userId);
        return Ok(cart);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] CartAddDto cartAddDto)
    {
        var userId = "test-user";
        await _cartService.AddToCartAsync(userId, cartAddDto);
        return Ok(new { Message = "Product added to cart successfully." });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateCartItem([FromBody] CartUpdateDto cartUpdateDto)
    {
        var userId = "test-user";
        await _cartService.UpdateCartItemAsync(userId, cartUpdateDto);
        return Ok(new { Message = "Cart item updated successfully." });
    }

    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveCartItem(int id)
    {
        var userId = "test-user";
        await _cartService.RemoveCartItemAsync(userId, id);
        return Ok(new { Message = "Cart item removed successfully." });
    }

    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
        var userId = "test-user";//var userId = User.Identity.Name;
        await _cartService.ClearCartAsync(userId);
        return Ok(new { Message = "Cart cleared successfully." });
    }
}