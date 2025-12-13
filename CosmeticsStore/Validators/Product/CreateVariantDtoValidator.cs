using CosmeticsStore.Dtos.Product;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CosmeticsStore.Validators.Product
{
    public class CreateVariantDtoValidator : AbstractValidator<AddProductRequest.CreateVariantDto>
    {
        public CreateVariantDtoValidator()
        {
            RuleFor(x => x.Sku)
                .NotEmpty().WithMessage("SKU is required.")
                .MaximumLength(100).WithMessage("SKU must not exceed 100 characters.");

            RuleFor(x => x.PriceAmount)
                .GreaterThanOrEqualTo(0).WithMessage("PriceAmount must be >= 0.");

            RuleFor(x => x.PriceCurrency)
                .NotEmpty().WithMessage("PriceCurrency is required.")
                .Must(BeValidCurrency).WithMessage("PriceCurrency must be a valid 3-letter code (e.g. EGP).");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be >= 0.");

            // IsActive is boolean, no validation needed beyond type constraints
        }

        private bool BeValidCurrency(string currency)
            => !string.IsNullOrWhiteSpace(currency) && Regex.IsMatch(currency, @"^[A-Z]{3}$");
    }
}
