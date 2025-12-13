using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;


namespace CosmeticsStore.Application.Payment.AddPayment
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, PaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;

        public AddPaymentCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponse> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new CosmeticsStore.Domain.Entities.Payment
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                Currency = request.Currency,
                Provider = request.Provider,
                TransactionId = request.TransactionId,
                Status = request.Status,
                CreatedOnUtc = DateTime.UtcNow,
                CreatedAtUtc = DateTime.UtcNow // maps to CreatedOnUtc via the property in entity
            };

            var created = await _paymentRepository.CreateAsync(payment, cancellationToken);

            return new PaymentResponse
            {
                PaymentId = created.Id,
                OrderId = created.OrderId,
                Amount = created.Amount,
                Currency = created.Currency,
                Provider = created.Provider,
                TransactionId = created.TransactionId,
                Status = created.Status,
                CreatedAtUtc = created.CreatedAtUtc,
                ModifiedAtUtc = created.ModifiedAtUtc
            };
        }
    }
}
