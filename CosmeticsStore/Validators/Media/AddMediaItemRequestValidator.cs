using CosmeticsStore.Dtos.Media;
using FluentValidation;

namespace CosmeticsStore.Validators.Media
{
    public class AddMediaItemRequestValidator : AbstractValidator<AddMediaItemRequest>
    {
        public AddMediaItemRequestValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(200).WithMessage("FileName must not exceed 200 characters.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Must(BeAValidUrl).WithMessage("Url must be a valid URL.");

            RuleFor(x => x.MediaType)
                .Matches(@"^[a-zA-Z0-9]+\/[a-zA-Z0-9\-\.\+]+$")
                .When(x => !string.IsNullOrWhiteSpace(x.MediaType))
                .WithMessage("MediaType must be a valid MIME type (e.g., image/png).");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0.");
        }

        private bool BeAValidUrl(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
