using CosmeticsStore.Dtos.Inventorys;
using FluentValidation;

namespace CosmeticsStore.Validators.Inventory
{
    public class AddInventoryItemRequestValidator : AbstractValidator<AddInventoryItemRequest>
    {
        public AddInventoryItemRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.");
        }
    }
}
