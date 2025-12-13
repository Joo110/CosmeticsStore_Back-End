using CosmeticsStore.Dtos.Media;
using FluentValidation;

namespace CosmeticsStore.Validators.Media
{
    public class UpdateMediaItemRequestValidator : AbstractValidator<UpdateMediaItemRequest>
    {
        public UpdateMediaItemRequestValidator()
        {
            RuleFor(x => x.FileName)
                .MaximumLength(200)
                .WithMessage("FileName must not exceed 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.FileName));

            RuleFor(x => x.Url)
                .Must(BeAValidUrl)
                .WithMessage("Url must be a valid URL.")
                .When(x => !string.IsNullOrWhiteSpace(x.Url));

            RuleFor(x => x.MediaType)
                .Matches(@"^[a-zA-Z0-9]+\/[a-zA-Z0-9\-\.\+]+$")
                .WithMessage("MediaType must be a valid MIME type (e.g., image/png).")
                .When(x => !string.IsNullOrWhiteSpace(x.MediaType));

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0.")
                .When(x => x.Size.HasValue);
        }

        private bool BeAValidUrl(string url)
            => Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
