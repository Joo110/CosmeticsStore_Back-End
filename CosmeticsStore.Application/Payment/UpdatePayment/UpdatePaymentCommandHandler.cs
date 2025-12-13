using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Payment.UpdatePayment
{
    public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, CosmeticsStore.Application.Payment.AddPayment.PaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;

        public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<CosmeticsStore.Application.Payment.AddPayment.PaymentResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);
            if (payment == null)
                throw new PaymentNotFoundException($"Payment with id {request.PaymentId} not found.");

            if (request.Amount.HasValue) payment.Amount = request.Amount.Value;
            if (request.Currency != null) payment.Currency = request.Currency;
            if (request.Provider != null) payment.Provider = request.Provider;
            if (request.TransactionId != null) payment.TransactionId = request.TransactionId;
            if (request.Status != null) payment.Status = request.Status;

            payment.ModifiedAtUtc = DateTime.UtcNow;

            await _paymentRepository.UpdateAsync(payment, cancellationToken);

            return new CosmeticsStore.Application.Payment.AddPayment.PaymentResponse
            {
                PaymentId = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                Currency = payment.Currency,
                Provider = payment.Provider,
                TransactionId = payment.TransactionId,
                Status = payment.Status,
                CreatedAtUtc = payment.CreatedAtUtc,
                ModifiedAtUtc = payment.ModifiedAtUtc
            };
        }
    }
}
