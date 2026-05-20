using Domain.Entities;

namespace Application.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartByUserIdAsync(string userId);
    Task AddCartItemAsync(CartItem cartItem);
    Task UpdateCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(int cartItemId);
    Task ClearCartAsync(int cartId);
    Task<bool> SaveChangesAsync();
    Task AddCartAsync(Cart cart);


}