using CosmeticsStore.Application.Carts.GetById;
using MediatR;

namespace CosmeticsStore.Application.Carts.AddItem
{
    public class AddItemCommand : IRequest<CartResponse>
    {
        public Guid UserId { get; }
        public Guid ProductVariantId { get; }
        public int Quantity { get; }

        public AddItemCommand(Guid userId, Guid productVariantId, int quantity)
        {
            UserId = userId;
            ProductVariantId = productVariantId;
            Quantity = quantity;
        }
    }
}