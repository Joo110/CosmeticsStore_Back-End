using CosmeticsStore.Dtos.Review;
using FluentValidation;

namespace CosmeticsStore.Validators.Review
{
    public class AddReviewRequestValidator : AbstractValidator<AddReviewRequest>
    {
        public AddReviewRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(2000)
                .WithMessage("Comment must not exceed 2000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Comment));
        }
    }
}
