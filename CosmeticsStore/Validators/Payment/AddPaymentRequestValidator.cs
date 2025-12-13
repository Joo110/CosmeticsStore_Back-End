using CosmeticsStore.Dtos.Payment;
using FluentValidation;

namespace CosmeticsStore.Validators.Payment
{
    public class AddPaymentRequestValidator : AbstractValidator<AddPaymentRequest>
    {
        public AddPaymentRequestValidator()
        {
            RuleFor(p => p.OrderId)
                .NotEmpty();

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(p => p.Currency)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(p => p.Provider)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(p => p.TransactionId)
                .MaximumLength(200)
                .When(p => p.TransactionId != null);

            RuleFor(p => p.Status)
                .NotEmpty()
                .Must(BeValidStatus)
                .WithMessage("Status must be one of: Pending, Completed, Failed.");
        }

        private bool BeValidStatus(string status)
        {
            var valid = new[] { "Pending", "Completed", "Failed" };
            return valid.Contains(status, StringComparer.OrdinalIgnoreCase);
        }
    }
}
