namespace Domain.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; } // Foreign key to Cart
        public Guid ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}