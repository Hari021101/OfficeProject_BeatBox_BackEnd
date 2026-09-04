namespace Domain.Entities;

public enum ReferralStatus
{
    Pending,
    Qualified,
    RewardCredited,
    Expired
}

public class Referral
{
    public int Id { get; set; }
    
    public string ReferrerId { get; set; } = string.Empty;
    public AppUser? Referrer { get; set; }

    public string? ReferredUserId { get; set; }
    public AppUser? ReferredUser { get; set; }

    public string ReferredUserEmail { get; set; } = string.Empty;
    public string ReferralCode { get; set; } = string.Empty;

    public ReferralStatus Status { get; set; } = ReferralStatus.Pending;
    public decimal RewardAmount { get; set; } = 500m;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? QualifiedDate { get; set; }
    public int? QualifyingOrderId { get; set; }
}
