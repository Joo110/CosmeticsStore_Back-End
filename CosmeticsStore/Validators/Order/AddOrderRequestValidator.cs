using CosmeticsStore.Dtos.Order;
using FluentValidation;

namespace CosmeticsStore.Validators.Order
{
    public class AddOrderRequestValidator : AbstractValidator<AddOrderRequest>
    {
        private static readonly string[] AllowedStatuses =
            new[] { "Draft", "Pending", "Confirmed", "Paid", "Shipped", "Cancelled", "Completed" };

        public AddOrderRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ShippingAddressId)
                .Must(BeNonEmptyGuid).When(x => x.ShippingAddressId.HasValue)
                .WithMessage("ShippingAddressId, if provided, must be a valid GUID.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .Must(s => AllowedStatuses.Contains(s))
                .WithMessage($"Status must be one of: {string.Join(", ", AllowedStatuses)}");

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Items must be provided.")
                .Must(items => items != null && items.Count > 0).WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items)
                .SetValidator(new CreateOrderItemDtoValidator());

            RuleFor(x => x.TotalCurrency)
                .NotEmpty().WithMessage("TotalCurrency is required.")
                .Must(BeValidCurrency).WithMessage("TotalCurrency must be a valid 3-letter ISO code (e.g. EGP).");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be >= 0.");

            // business rule: if items provided, ensure all item currencies match TotalCurrency
            RuleFor(x => x)
                .Must(req => ItemsCurrenciesMatchTotal(req))
                .When(req => req.Items != null && req.Items.Count > 0)
                .WithMessage("All item currencies must match TotalCurrency.");

            // business rule: ensure TotalAmount equals sum of (unitPrice * quantity)
            RuleFor(x => x)
                .Must(req => TotalMatchesItemsSum(req))
                .When(req => req.Items != null && req.Items.Count > 0)
                .WithMessage("TotalAmount must equal the sum of (UnitPrice * Quantity) of all items.");
        }

        private static bool BeNonEmptyGuid(Guid? id) => id.HasValue && id.Value != Guid.Empty;

        private static bool BeValidCurrency(string currency)
            => !string.IsNullOrWhiteSpace(currency) && System.Text.RegularExpressions.Regex.IsMatch(currency, @"^[A-Z]{3}$");

        private static bool ItemsCurrenciesMatchTotal(AddOrderRequest req)
        {
            if (req.Items == null || string.IsNullOrWhiteSpace(req.TotalCurrency)) return true;
            return req.Items.All(i => string.Equals(i.Currency, req.TotalCurrency, StringComparison.OrdinalIgnoreCase));
        }

        private static bool TotalMatchesItemsSum(AddOrderRequest req)
        {
            if (req.Items == null) return true;

            decimal sum = req.Items.Sum(i => i.UnitPrice * i.Quantity);
            // exact decimal comparison is fine for money represented as decimal in C#
            return req.TotalAmount == sum;
        }
    }
}
