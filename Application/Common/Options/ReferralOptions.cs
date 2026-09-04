namespace Application.Common.Options;

public class ReferralOptions
{
    public const string SectionName = "Referral";

    public decimal DefaultRewardAmount { get; set; } = 500.00m;
    public decimal MinimumQualifyingOrderTotal { get; set; } = 0.00m;
}

public class FrontendOptions
{
    public const string SectionName = "Frontend";

    public string BaseUrl { get; set; } = "http://localhost:5173";
}
