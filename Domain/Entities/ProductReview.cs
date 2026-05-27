namespace Domain.Entities;

public class ProductReview
{
    public int Id { get; set; }

    public Guid ProductId { get; set; }

    public string UserId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsVerifiedPurchase { get; set; }

    public Product Product { get; set; }

    public AppUser User { get; set; }
}