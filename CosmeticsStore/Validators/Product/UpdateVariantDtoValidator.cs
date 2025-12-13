using CosmeticsStore.Dtos.Product;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CosmeticsStore.Validators.Product
{
    public class UpdateVariantDtoValidator : AbstractValidator<UpdateProductRequest.UpdateVariantDto>
    {
        public UpdateVariantDtoValidator()
        {
            RuleFor(x => x.ProductVariantId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("ProductVariantId, if provided, must be a valid GUID.");

            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("SKU is required.")
                .MaximumLength(100);

            RuleFor(x => x.PriceAmount)
                .GreaterThanOrEqualTo(0).WithMessage("PriceAmount must be >= 0.");

            RuleFor(x => x.PriceCurrency)
                .NotEmpty().WithMessage("PriceCurrency is required.")
                .Must(BeValidCurrency).WithMessage("PriceCurrency must be a valid 3-letter code (e.g. EGP).");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be >= 0.");
        }

        private bool BeValidCurrency(string currency)
            => !string.IsNullOrWhiteSpace(currency) && Regex.IsMatch(currency, @"^[A-Z]{3}$");
    }
}
