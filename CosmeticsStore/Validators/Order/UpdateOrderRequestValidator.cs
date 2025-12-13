using CosmeticsStore.Dtos.Order;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CosmeticsStore.Validators.Order
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        private static readonly string[] AllowedStatuses =
            new[] { "Draft", "Pending", "Confirmed", "Paid", "Shipped", "Cancelled", "Completed" };

        public UpdateOrderRequestValidator()
        {
            // Status is optional, but if provided must be valid
            RuleFor(x => x.Status)
                .Must(status => string.IsNullOrEmpty(status) || AllowedStatuses.Contains(status))
                .WithMessage($"Status must be one of: {string.Join(", ", AllowedStatuses)}");

            RuleFor(x => x.ShippingAddressId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("ShippingAddressId, if provided, must be a valid GUID.");

            RuleFor(x => x.Items)
                .Must(items => items == null || items.Count > 0)
                .WithMessage("If Items is provided it must contain at least one item.");

            RuleForEach(x => x.Items)
                .SetValidator(new UpdateOrderItemDtoValidator());

            RuleFor(x => x.TotalCurrency)
                .Must(c => string.IsNullOrEmpty(c) || BeValidCurrency(c))
                .WithMessage("TotalCurrency must be a valid 3-letter ISO code (e.g. EGP).");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).When(x => x.TotalAmount.HasValue)
                .WithMessage("TotalAmount must be >= 0.");

            // If items provided and TotalCurrency provided, ensure currencies match
            RuleFor(x => x)
                .Must(req => Update_ItemsCurrenciesMatchTotal(req))
                .When(req => req.Items != null && req.Items.Count > 0 && !string.IsNullOrWhiteSpace(req.TotalCurrency))
                .WithMessage("All item currencies must match TotalCurrency.");

            // If items provided and TotalAmount provided, ensure TotalAmount equals sum of items
            RuleFor(x => x)
                .Must(req => Update_TotalMatchesItemsSum(req))
                .When(req => req.Items != null && req.Items.Count > 0 && req.TotalAmount.HasValue)
                .WithMessage("TotalAmount must equal the sum of (UnitPrice * Quantity) of all items.");
        }

        private static bool BeValidCurrency(string currency)
            => !string.IsNullOrWhiteSpace(currency) && Regex.IsMatch(currency, @"^[A-Z]{3}$");

        private static bool Update_ItemsCurrenciesMatchTotal(UpdateOrderRequest req)
        {
            if (req.Items == null || string.IsNullOrWhiteSpace(req.TotalCurrency)) return true;
            return req.Items.All(i => string.Equals(i.Currency, req.TotalCurrency, StringComparison.OrdinalIgnoreCase));
        }

        private static bool Update_TotalMatchesItemsSum(UpdateOrderRequest req)
        {
            if (req.Items == null || !req.TotalAmount.HasValue) return true;
            decimal sum = req.Items.Sum(i => i.UnitPrice * i.Quantity);
            return req.TotalAmount.Value == sum;
        }
    }
}
