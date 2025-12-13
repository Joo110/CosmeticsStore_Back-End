using CosmeticsStore.Dtos.Product;
using FluentValidation;

namespace CosmeticsStore.Validators.Product
{
    public class UpdateMediaDtoValidator : AbstractValidator<UpdateProductRequest.UpdateMediaDto>
    {
        public UpdateMediaDtoValidator()
        {
            RuleFor(x => x.MediaId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("MediaId, if provided, must be a valid GUID.");

            RuleFor(x => x.Url)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.Url))
                .WithMessage("Url must be a valid absolute URL.");

            RuleFor(x => x.FileName)
                .MaximumLength(200).When(x => !string.IsNullOrWhiteSpace(x.FileName));

            RuleFor(x => x.ContentType)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.ContentType));

            RuleFor(x => x.SizeInBytes)
                .GreaterThan(0).When(x => x.SizeInBytes > 0)
                .WithMessage("SizeInBytes must be greater than 0 when provided.");
        }

        private bool BeAValidUrl(string url) => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
