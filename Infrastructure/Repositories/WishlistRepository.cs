using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly AppDbContext _context;

    public WishlistRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WishlistItem>> GetWishlistByUserIdAsync(string userId)
    {
        return await _context.WishlistItems
            .Include(w => w.Product)
            .Where(w => w.UserId == userId)
            .ToListAsync();
    }

    public async Task<WishlistItem?> GetWishlistItemAsync(string userId, Guid productId)
    {
        return await _context.WishlistItems
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
    }

    public async Task AddWishlistItemAsync(WishlistItem item)
    {
        await _context.WishlistItems.AddAsync(item);
    }

    public Task RemoveWishlistItemAsync(WishlistItem item)
    {
        _context.WishlistItems.Remove(item);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
