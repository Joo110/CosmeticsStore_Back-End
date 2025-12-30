using CosmeticsStore.Application.Carts.Common;
using CosmeticsStore.Application.Carts.GetById;
using MediatR;

namespace CosmeticsStore.Application.Carts.Create
{
    public class CreateCartCommand : IRequest<CartResponse>
    {
        public Guid UserId { get; }
        public List<CartItemDto> Items { get; }

        public CreateCartCommand(Guid userId, List<CartItemDto> items)
        {
            UserId = userId;
            Items = items;
        }
    }
}
