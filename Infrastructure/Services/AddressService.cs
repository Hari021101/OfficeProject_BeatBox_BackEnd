using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMapper _mapper;

    public AddressService(IAddressRepository addressRepository, IMapper mapper)
    {
        _addressRepository = addressRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserAddressDto>> GetAddressesAsync(string userId)
    {
        var addresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<UserAddressDto>>(addresses);
    }

    public async Task<UserAddressDto> GetAddressByIdAsync(int id, string userId)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id, userId);
        if (address == null) throw new Exception("Address not found");
        return _mapper.Map<UserAddressDto>(address);
    }

    public async Task<UserAddressDto> AddAddressAsync(string userId, UserAddressDto dto)
    {
        var address = _mapper.Map<UserAddress>(dto);
        address.UserId = userId;

        var currentAddresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
        if (!currentAddresses.Any())
        {
            address.IsDefault = true;
        }
        else if (dto.IsDefault)
        {
            foreach (var a in currentAddresses)
            {
                a.IsDefault = false;
                await _addressRepository.UpdateAddressAsync(a);
            }
        }

        var addedAddress = await _addressRepository.AddAddressAsync(address);
        await _addressRepository.SaveChangesAsync();

        return _mapper.Map<UserAddressDto>(addedAddress);
    }

    public async Task UpdateAddressAsync(int id, string userId, UserAddressDto dto)
    {
        var address = await _addressRepository.GetAddressByIdAsync(id, userId);
        if (address == null) throw new Exception("Address not found");

        if (dto.IsDefault && !address.IsDefault)
        {
            var currentAddresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
            foreach (var a in currentAddresses.Where(a => a.UserAddressId != id))
            {
                a.IsDefault = false;
                await _addressRepository.UpdateAddressAsync(a);
            }
        }

        _mapper.Map(dto, address);
        address.UserAddressId = id; // prevent overwrite
        address.UserId = userId; // prevent overwrite

        await _addressRepository.UpdateAddressAsync(address);
        await _addressRepository.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(int id, string userId)
    {
        await _addressRepository.DeleteAddressAsync(id, userId);
        await _addressRepository.SaveChangesAsync();
    }

    public async Task SetDefaultAddressAsync(int id, string userId)
    {
        var currentAddresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
        
        foreach (var a in currentAddresses)
        {
            a.IsDefault = a.UserAddressId == id;
            await _addressRepository.UpdateAddressAsync(a);
        }

        await _addressRepository.SaveChangesAsync();
    }
}
