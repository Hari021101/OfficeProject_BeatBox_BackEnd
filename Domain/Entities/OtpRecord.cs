namespace Domain.Entities;

public enum OtpType { Email, Phone }

public class OtpRecord
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public OtpType Type { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsUsed { get; set; }
}
