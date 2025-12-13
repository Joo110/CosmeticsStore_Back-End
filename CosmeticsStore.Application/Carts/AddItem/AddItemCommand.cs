using CosmeticsStore.Application.Carts.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.AddItem
{
    public class AddItemCommand : IRequest<CartResponse>
    {
        public Guid UserId { get; }
        public CartItemDto Item { get; }

        public AddItemCommand(Guid userId, CartItemDto item)
        {
            UserId = userId;
            Item = item;
        }
    }

    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
