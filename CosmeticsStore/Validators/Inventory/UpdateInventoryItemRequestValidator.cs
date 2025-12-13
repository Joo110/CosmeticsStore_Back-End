using CosmeticsStore.Dtos.Inventorys;
using FluentValidation;

namespace CosmeticsStore.Validators.Inventory
{
    public class UpdateInventoryItemRequestValidator : AbstractValidator<UpdateInventoryItemRequest>
    {
        public UpdateInventoryItemRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.Location)
                .MaximumLength(200)
                .WithMessage("Location must not exceed 200 characters.");
        }
    }
}
