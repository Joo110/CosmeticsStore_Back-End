namespace CosmeticsStore.Dtos.Payment
{
    public class AddPaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";
        public string Provider { get; set; } = null!; // e.g. Stripe, PayPal
        public string? TransactionId { get; set; }
        public string Status { get; set; } = "Pending";
    }

    public class UpdatePaymentRequest
    {
        // Partial update fields (all optional)
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public string? Provider { get; set; }
        public string? TransactionId { get; set; }
        public string? Status { get; set; }
    }

    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";

        public string Status { get; set; } = default!;
        public string Provider { get; set; } = default!;
        public string? TransactionId { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
