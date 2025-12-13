using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Payment.UpdatePayment
{
    public class UpdatePaymentCommand : IRequest<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>
    {
        public Guid PaymentId { get; set; }

        // Partial update: optional fields
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public string? Provider { get; set; }
        public string? TransactionId { get; set; }
        public string? Status { get; set; }
    }
}
