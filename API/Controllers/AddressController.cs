using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAddresses()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var addresses = await _addressService.GetAddressesAsync(userId);
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddress(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var address = await _addressService.GetAddressByIdAsync(id, userId);
        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] UserAddressDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var created = await _addressService.AddAddressAsync(userId, dto);
        return Ok(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] UserAddressDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _addressService.UpdateAddressAsync(id, userId, dto);
        return Ok(new { Message = "Address updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _addressService.DeleteAddressAsync(id, userId);
        return Ok(new { Message = "Address deleted successfully." });
    }

    [HttpPut("{id}/default")]
    public async Task<IActionResult> SetDefaultAddress(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _addressService.SetDefaultAddressAsync(id, userId);
        return Ok(new { Message = "Default address updated." });
    }
}
