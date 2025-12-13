using CosmeticsStore.Dtos.Product;
using FluentValidation;

namespace CosmeticsStore.Validators.Product
{
    public class CreateMediaDtoValidator : AbstractValidator<AddProductRequest.CreateMediaDto>
    {
        public CreateMediaDtoValidator()
        {
            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Must(BeAValidUrl).WithMessage("Url must be a valid absolute URL.");

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(200).WithMessage("FileName must not exceed 200 characters.");

            RuleFor(x => x.ContentType)
                .NotEmpty().WithMessage("ContentType is required.")
                .MaximumLength(100).WithMessage("ContentType must not exceed 100 characters.");

            RuleFor(x => x.SizeInBytes)
                .GreaterThan(0).WithMessage("SizeInBytes must be greater than 0.");

            // IsPrimary is boolean
        }

        private bool BeAValidUrl(string url) => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
