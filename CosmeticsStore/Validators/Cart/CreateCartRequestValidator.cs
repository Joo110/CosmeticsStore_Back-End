using CosmeticsStore.Dtos.Cart;
using FluentValidation;

namespace CosmeticsStore.Validators.Cart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");

            RuleFor(x => x.Items)
                .NotNull()
                .WithMessage("Items list cannot be null.")
                .Must(items => items!.Count > 0)
                .WithMessage("Cart must contain at least one item.");

            // Apply CartItemRequestValidator to each item
            RuleForEach(x => x.Items)
                .SetValidator(new CartItemRequestValidator());
        }
    }
}
