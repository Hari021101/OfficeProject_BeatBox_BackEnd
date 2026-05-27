using Application.DTOs;

namespace Application.Interfaces;

public interface IWishlistService
{
    Task<IEnumerable<WishlistItemDto>> GetWishlistAsync(string userId);
    Task ToggleWishlistItemAsync(string userId, Guid productId);
    Task ClearWishlistAsync(string userId);
}
