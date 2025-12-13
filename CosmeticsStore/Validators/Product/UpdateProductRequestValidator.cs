using CosmeticsStore.Dtos.Product;
using FluentValidation;
using System.Linq;

namespace CosmeticsStore.Validators.Product
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(250).When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Slug)
                .MaximumLength(250).When(x => !string.IsNullOrWhiteSpace(x.Slug));

            RuleFor(x => x.Description)
                .MaximumLength(4000).When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.CategoryId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("CategoryId, if provided, must be a valid GUID.");

            // Variants
            RuleFor(x => x.Variants)
                .Must(v => v == null || v.Count > 0)
                .WithMessage("If Variants provided, it must contain at least one item.");

            RuleForEach(x => x.Variants)
                .SetValidator(new UpdateVariantDtoValidator());

            RuleFor(x => x)
                .Must(x => UpdateVariantsHaveUniqueSkus(x.Variants))
                .WithMessage("Variant SKUs must be unique within the product.")
                .When(x => x.Variants != null && x.Variants.Count > 0);

            // Media
            RuleForEach(x => x.Media)
                .SetValidator(new UpdateMediaDtoValidator());

            RuleFor(x => x)
                .Must(x => UpdateMediaHasSinglePrimary(x.Media))
                .WithMessage("Media can contain at most one primary item.")
                .When(x => x.Media != null && x.Media.Count > 0);
        }

        private static bool UpdateVariantsHaveUniqueSkus(System.Collections.Generic.List<UpdateProductRequest.UpdateVariantDto>? variants)
        {
            if (variants == null) return true;
            var skus = variants.Select(v => v.Sku?.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();
            return skus.Count == skus.Distinct(System.StringComparer.OrdinalIgnoreCase).Count();
        }

        private static bool UpdateMediaHasSinglePrimary(System.Collections.Generic.List<UpdateProductRequest.UpdateMediaDto>? media)
        {
            if (media == null) return true;
            return media.Count(m => m.IsPrimary) <= 1;
        }
    }
}
