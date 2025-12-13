using CosmeticsStore.Dtos.Review;
using FluentValidation;

namespace CosmeticsStore.Validators.Review
{
    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidator()
        {
            // Rating optional but if provided must be valid
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.")
                .When(x => x.Rating.HasValue);

            // Comment optional but with max length
            RuleFor(x => x.Comment)
                .MaximumLength(2000)
                .WithMessage("Comment must not exceed 2000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Comment));
        }
    }
}
