using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class WishlistService : IWishlistService
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IMapper _mapper;

    public WishlistService(IWishlistRepository wishlistRepository, IMapper mapper)
    {
        _wishlistRepository = wishlistRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WishlistItemDto>> GetWishlistAsync(string userId)
    {
        var items = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<WishlistItemDto>>(items);
    }

    public async Task ToggleWishlistItemAsync(string userId, Guid productId)
    {
        var existingItem = await _wishlistRepository.GetWishlistItemAsync(userId, productId);
        if (existingItem != null)
        {
            await _wishlistRepository.RemoveWishlistItemAsync(existingItem);
        }
        else
        {
            var newItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
                AddedDate = DateTime.UtcNow
            };
            await _wishlistRepository.AddWishlistItemAsync(newItem);
        }
        await _wishlistRepository.SaveChangesAsync();
    }

    public async Task ClearWishlistAsync(string userId)
    {
        var items = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
        foreach (var item in items)
        {
            await _wishlistRepository.RemoveWishlistItemAsync(item);
        }
        await _wishlistRepository.SaveChangesAsync();
    }
}
