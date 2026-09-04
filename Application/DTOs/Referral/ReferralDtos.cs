namespace Application.DTOs.Referral;

public class ReferralDashboardDto
{
    public string ReferralCode { get; set; } = string.Empty;
    public string ReferralLink { get; set; } = string.Empty;
    public int FriendsInvited { get; set; }
    public int SuccessfulReferrals { get; set; }
    public decimal TotalRewardsEarned { get; set; }
    public List<ReferralHistoryItemDto> History { get; set; } = new();
}

public class ReferralHistoryItemDto
{
    public int Id { get; set; }
    public string FriendName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? CouponCode { get; set; }
    public decimal RewardAmount { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class ReferralValidationResultDto
{
    public bool IsValid { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? ReferrerName { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class ApplyReferralResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? ReferralId { get; set; }
    public string? CouponCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

public class ValidateReferralRequest
{
    public string Code { get; set; } = string.Empty;
}

public class ApplyReferralRequest
{
    public string Code { get; set; } = string.Empty;
}

public class ReferralEligibilityDto
{
    public bool IsEligible { get; set; }
    public decimal DiscountAmount { get; set; }
    public string ReferralCode { get; set; } = string.Empty;
    public string? CouponCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
