namespace Domain.Entities;

public class WishlistItem
{
    public int WishlistItemId { get; set; }
    public string UserId { get; set; } // Foreign Key to AppUser
    public Guid ProductId { get; set; } // Foreign Key to Product
    
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    public AppUser User { get; set; }
    public Product Product { get; set; }
}
