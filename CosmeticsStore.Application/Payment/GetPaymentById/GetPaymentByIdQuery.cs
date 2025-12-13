using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmeticsStore.Application.Payment;

namespace CosmeticsStore.Application.Payment.GetPaymentById
{
    public class GetPaymentByIdQuery : IRequest<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>
    {
        public Guid PaymentId { get; set; }
    }
}
