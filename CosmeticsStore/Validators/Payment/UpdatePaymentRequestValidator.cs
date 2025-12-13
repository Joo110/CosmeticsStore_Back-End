using FluentValidation;

namespace CosmeticsStore.Validators.Payment
{
    public class UpdatePaymentRequestValidator : AbstractValidator<CosmeticsStore.Dtos.Payment.UpdatePaymentRequest>
    {
        public UpdatePaymentRequestValidator()
        {
            RuleFor(p => p.Amount)
                .GreaterThan(0)
                .When(p => p.Amount.HasValue);

            RuleFor(p => p.Currency)
                .MaximumLength(10)
                .When(p => p.Currency != null);

            RuleFor(p => p.Provider)
                .MaximumLength(100)
                .When(p => p.Provider != null);

            RuleFor(p => p.TransactionId)
                .MaximumLength(200)
                .When(p => p.TransactionId != null);

            RuleFor(p => p.Status)
                .Must(BeValidStatus)
                .When(p => p.Status != null)
                .WithMessage("Status must be one of: Pending, Completed, Failed.");
        }

        private bool BeValidStatus(string status)
        {
            var valid = new[] { "Pending", "Completed", "Failed" };
            return valid.Contains(status, StringComparer.OrdinalIgnoreCase);
        }
    }
}