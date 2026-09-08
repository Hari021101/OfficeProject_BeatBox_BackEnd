namespace Application.Common.Options;

public class ReferralOptions
{
    public const string SectionName = "Referral";

    public decimal RewardAmount { get; set; } = 500.00m;
    public decimal DefaultRewardAmount
    {
        get => RewardAmount;
        set => RewardAmount = value;
    }
    public int FriendCouponValidityDays { get; set; } = 14;
    public int ReferrerCouponValidityDays { get; set; } = 30;
    public decimal MinimumQualifyingOrderTotal { get; set; } = 0.00m;
    public bool Enabled { get; set; } = true;
}

public class FrontendOptions
{
    public const string SectionName = "Frontend";

    public string BaseUrl { get; set; } = string.Empty;
}
