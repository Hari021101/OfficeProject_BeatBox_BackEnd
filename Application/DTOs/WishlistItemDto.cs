namespace Application.DTOs;

public class WishlistItemDto
{
    public int WishlistItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductImage { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public decimal? ProductDiscountPrice { get; set; }
    public DateTime AddedDate { get; set; }
}
