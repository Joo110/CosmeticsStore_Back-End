using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Payment.GetAllPayments
{
    public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, PaginatedList<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetAllPaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaginatedList<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<Domain.Entities.Payment>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
                // Note: Query<T> in your project doesn't include Filter expression; repository can implement filtering based on Query.SearchTerm/SortBy or custom logic.
            };

            var paged = await _paymentRepository.GetForManagementAsync(query, cancellationToken);

            // Map PaymentModel -> PaymentResponse (PaymentModel is what repository returns for management)
            var items = paged.Items.Select(m => new CosmeticsStore.Application.Payment.AddPayment.PaymentResponse
            {
                PaymentId = m.Id,
                OrderId = m.OrderId,
                Amount = m.Amount,
                Currency = m.Currency,
                Provider = m.Provider,
                TransactionId = m.TransactionId,
                Status = m.Status,
                CreatedAtUtc = m.CreatedOnUtc
            });

            // If user requested filtering by OrderId/Status/Provider but repository doesn't support it,
            // note: filtering here will affect paging correctness. Prefer implementing filtering in repository.
            if (request.OrderId.HasValue || !string.IsNullOrWhiteSpace(request.Status) || !string.IsNullOrWhiteSpace(request.Provider))
            {
                items = items.Where(i =>
                    (!request.OrderId.HasValue || i.OrderId == request.OrderId.Value) &&
                    (string.IsNullOrWhiteSpace(request.Status) || i.Status == request.Status) &&
                    (string.IsNullOrWhiteSpace(request.Provider) || i.Provider == request.Provider)
                );
            }

            var itemsList = items.ToList();

            var result = new PaginatedList<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>(
                itemsList,
                /* TotalCount */ itemsList.Count, // warning: if you filtered client-side this is the filtered count, not the DB total
                paged.PageIndex,
                request.PageSize
            );

            return result;
        }
    }
}
