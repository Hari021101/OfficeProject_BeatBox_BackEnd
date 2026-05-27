using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserAddress>> GetAddressesByUserIdAsync(string userId)
    {
        return await _context.UserAddresses
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<UserAddress?> GetAddressByIdAsync(int id, string userId)
    {
        return await _context.UserAddresses
            .FirstOrDefaultAsync(a => a.UserAddressId == id && a.UserId == userId);
    }

    public async Task<UserAddress> AddAddressAsync(UserAddress address)
    {
        await _context.UserAddresses.AddAsync(address);
        return address;
    }

    public Task UpdateAddressAsync(UserAddress address)
    {
        _context.UserAddresses.Update(address);
        return Task.CompletedTask;
    }

    public async Task DeleteAddressAsync(int id, string userId)
    {
        var address = await GetAddressByIdAsync(id, userId);
        if (address != null)
        {
            _context.UserAddresses.Remove(address);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
