using CosmeticsStore.Dtos.Order;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CosmeticsStore.Validators.Order
{
    public class UpdateOrderItemDtoValidator : AbstractValidator<UpdateOrderRequest.UpdateOrderItemDto>
    {
        public UpdateOrderItemDtoValidator()
        {
            // OrderItemId can be null (means new item), but if provided it must be valid GUID
            RuleFor(x => x.OrderItemId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("OrderItemId, if provided, must be a valid GUID.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("UnitPrice must be greater than or equal to 0.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Must(BeValidCurrency).WithMessage("Currency must be a valid 3-letter ISO code (e.g. EGP).");
        }

        private bool BeValidCurrency(string currency)
            => !string.IsNullOrWhiteSpace(currency) && Regex.IsMatch(currency, @"^[A-Z]{3}$");
    }
}
