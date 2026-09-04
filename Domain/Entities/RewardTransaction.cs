namespace Domain.Entities;

public class RewardTransaction
{
    public int Id { get; set; }
    
    public string UserId { get; set; } = string.Empty;
    public AppUser? User { get; set; }

    public int? ReferralId { get; set; }
    public Referral? Referral { get; set; }

    public int? OrderId { get; set; }
    public Order? Order { get; set; }

    public decimal Amount { get; set; }
    
    /// <summary>ReferralCredit | RewardRedemption | Adjustment</summary>
    public string TransactionType { get; set; } = "ReferralCredit";

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
