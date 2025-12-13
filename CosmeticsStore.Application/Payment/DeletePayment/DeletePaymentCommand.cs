using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Payment.DeletePayment
{
    public class DeletePaymentCommand : IRequest<Unit>
    {
        public Guid PaymentId { get; set; }
    }
}
