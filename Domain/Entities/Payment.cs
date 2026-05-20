namespace Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
        public decimal Amount { get; set; }
        public string Status { get; set; } // Enum: Success, Failed, etc.
        public string Method { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedDate { get; set; }

        public Order Order { get; set; }
    }
}