using CosmeticsStore.Application.Carts.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
