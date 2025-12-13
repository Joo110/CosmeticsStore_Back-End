using System;

namespace CosmeticsStore.Application.Payment.AddPayment
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";

        public string Status { get; set; } = default!; // Pending, Succeeded, Failed, Refunded
        public string Provider { get; set; } = default!;
        public string? TransactionId { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ModifiedAtUtc { get; set; }
    }
}
