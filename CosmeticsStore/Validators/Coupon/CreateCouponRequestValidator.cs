using CosmeticsStore.Dtos.Coupont;
using FluentValidation;

namespace CosmeticsStore.Validators.Coupon
{
    public class CreateCouponRequestValidator : AbstractValidator<CreateCouponRequest>
    {
        public CreateCouponRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Coupon code is required.")
                .MaximumLength(50).WithMessage("Coupon code must not exceed 50 characters.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThan(0).WithMessage("Discount amount must be greater than 0.");

            RuleFor(x => x.ValidFrom)
                .NotEmpty().WithMessage("ValidFrom date is required.");

            RuleFor(x => x.ValidTo)
                .GreaterThan(x => x.ValidFrom)
                .WithMessage("ValidTo must be later than ValidFrom.");
        }
    }
}
