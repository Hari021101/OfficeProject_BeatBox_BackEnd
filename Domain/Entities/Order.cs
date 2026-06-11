namespace Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; } // Foreign key to AppUser
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Enum: Pending, Processing, etc.
        public string ShippingAddress { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public string? PromoCode { get; set; }

        public decimal DiscountAmount { get; set; }

    }
}