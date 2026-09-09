using Application.DTOs.Referral;

namespace Application.Interfaces;

public interface IReferralService
{
    Task<string> GetUserReferralCodeAsync(string userId, CancellationToken cancellationToken = default);
    Task<ReferralDashboardDto> GetReferralDashboardAsync(string userId, CancellationToken cancellationToken = default);
    Task<ReferralValidationResultDto> ValidateReferralCodeAsync(string code, string? currentUserId, CancellationToken cancellationToken = default);
    Task<ApplyReferralResultDto> ApplyReferralAsync(string code, string referredUserId, CancellationToken cancellationToken = default);
    Task<ReferralEligibilityDto> GetReferralEligibilityAsync(string userId, CancellationToken cancellationToken = default);
    Task ProcessQualifyingOrderAsync(int orderId, string userId, decimal orderTotal, CancellationToken cancellationToken = default);
}
