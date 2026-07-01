using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IBusinessEventPublisher _eventPublisher;

    public ProfileService(UserManager<AppUser> userManager, IMapper mapper, IBusinessEventPublisher eventPublisher)
    {
        _userManager = userManager;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<UserProfileDto> GetProfileAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        return _mapper.Map<UserProfileDto>(user);
    }

    public async Task UpdateProfileAsync(string userId, UserProfileUpdateDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        user.FullName = dto.FullName;
        user.PhoneNumber = dto.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update profile: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "User",
            EntityId = user.Id,
            Title = user.Email ?? user.UserName ?? "User",
            Description = "User profile details updated",
            Icon = "User",
            ColorClass = "text-info",
            BgClass = "bg-info"
        });
    }

    public async Task ChangePasswordAsync(string userId, ChangePasswordDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new Exception("User not found");

        var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to change password: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        await _eventPublisher.PublishAsync(new Application.Common.Events.BusinessEvent
        {
            ActionType = "UPDATED",
            EntityType = "User",
            EntityId = user.Id,
            Title = user.Email ?? user.UserName ?? "User",
            Description = "User password changed successfully",
            Icon = "Key",
            ColorClass = "text-warning",
            BgClass = "bg-warning"
        });
    }
}
