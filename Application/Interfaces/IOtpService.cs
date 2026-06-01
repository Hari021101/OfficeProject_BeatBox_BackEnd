using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IOtpService
{
    Task SendEmailOtpAsync(string userId, string email);
    Task SendPhoneOtpAsync(string userId, string phoneNumber);
    Task<bool> VerifyOtpAsync(string userId, string code, OtpType type);
}
