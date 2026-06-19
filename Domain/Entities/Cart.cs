namespace Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}