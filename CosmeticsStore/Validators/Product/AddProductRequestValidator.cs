using CosmeticsStore.Dtos.Product;
using FluentValidation;
using System.Linq;

namespace CosmeticsStore.Validators.Product
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(250).WithMessage("Name must not exceed 250 characters.");

            RuleFor(x => x.Slug)
                .MaximumLength(250).When(x => !string.IsNullOrWhiteSpace(x.Slug))
                .WithMessage("Slug must not exceed 250 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(4000).When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");

            // Variants - optional but if present must contain valid items and no duplicate SKUs
            RuleFor(x => x.Variants)
                .Must(v => v == null || v.Count > 0)
                .WithMessage("If Variants provided, it must contain at least one item.");

            RuleForEach(x => x.Variants)
                .SetValidator(new CreateVariantDtoValidator());

            RuleFor(x => x)
                .Must(x => VariantsHaveUniqueSkus(x.Variants))
                .WithMessage("Variant SKUs must be unique within the product.")
                .When(x => x.Variants != null && x.Variants.Count > 0);

            // Media - optional; if provided validate each item and ensure at most one primary
            RuleForEach(x => x.Media)
                .SetValidator(new CreateMediaDtoValidator());

            RuleFor(x => x)
                .Must(x => MediaHasSinglePrimary(x.Media))
                .WithMessage("Media can contain at most one primary item.")
                .When(x => x.Media != null && x.Media.Count > 0);

            // IsPublished is boolean
        }

        private static bool VariantsHaveUniqueSkus(System.Collections.Generic.List<AddProductRequest.CreateVariantDto>? variants)
        {
            if (variants == null) return true;
            var skus = variants.Select(v => v.Sku?.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();
            return skus.Count == skus.Distinct(StringComparer.OrdinalIgnoreCase).Count();
        }

        private static bool MediaHasSinglePrimary(System.Collections.Generic.List<AddProductRequest.CreateMediaDto>? media)
        {
            if (media == null) return true;
            return media.Count(m => m.IsPrimary) <= 1;
        }
    }
}
