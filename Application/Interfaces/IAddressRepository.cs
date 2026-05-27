using Domain.Entities;

namespace Application.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<UserAddress>> GetAddressesByUserIdAsync(string userId);
    Task<UserAddress?> GetAddressByIdAsync(int id, string userId);
    Task<UserAddress> AddAddressAsync(UserAddress address);
    Task UpdateAddressAsync(UserAddress address);
    Task DeleteAddressAsync(int id, string userId);
    Task SaveChangesAsync();
}
