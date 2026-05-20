namespace Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
        public Guid ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}