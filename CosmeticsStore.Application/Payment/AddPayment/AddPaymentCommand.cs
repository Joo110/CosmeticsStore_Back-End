using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Payment.AddPayment
{
    public class AddPaymentCommand : IRequest<PaymentResponse>
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";
        public string Provider { get; set; } = null!; // Stripe, PayPal, ...
        public string? TransactionId { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
