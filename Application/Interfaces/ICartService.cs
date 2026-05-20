using Application.DTOs;

namespace Application.Interfaces;

public interface ICartService
{
    Task<CartDto> GetCartAsync(string userId);
    Task AddToCartAsync(string userId, CartAddDto cartAddDto);
    Task UpdateCartItemAsync(string userId, CartUpdateDto cartUpdateDto);
    Task RemoveCartItemAsync(string userId, int cartItemId);
    Task ClearCartAsync(string userId);
}