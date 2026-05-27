using Application.DTOs;

namespace Application.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<UserAddressDto>> GetAddressesAsync(string userId);
    Task<UserAddressDto> GetAddressByIdAsync(int id, string userId);
    Task<UserAddressDto> AddAddressAsync(string userId, UserAddressDto dto);
    Task UpdateAddressAsync(int id, string userId, UserAddressDto dto);
    Task DeleteAddressAsync(int id, string userId);
    Task SetDefaultAddressAsync(int id, string userId);
}
