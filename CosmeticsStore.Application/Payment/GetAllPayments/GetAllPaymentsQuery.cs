using CosmeticsStore.Domain.Models;
using MediatR;

namespace CosmeticsStore.Application.Payment.GetAllPayments
{
    public class GetAllPaymentsQuery : IRequest<PaginatedList<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public Guid? OrderId { get; set; } // optional filter
        public string? Status { get; set; } // optional filter
        public string? Provider { get; set; } // optional filter
    }
}
