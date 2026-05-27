using Domain.Entities;

namespace Application.Interfaces;

public interface IWishlistRepository
{
    Task<IEnumerable<WishlistItem>> GetWishlistByUserIdAsync(string userId);
    Task<WishlistItem?> GetWishlistItemAsync(string userId, Guid productId);
    Task AddWishlistItemAsync(WishlistItem item);
    Task RemoveWishlistItemAsync(WishlistItem item);
    Task SaveChangesAsync();
}
