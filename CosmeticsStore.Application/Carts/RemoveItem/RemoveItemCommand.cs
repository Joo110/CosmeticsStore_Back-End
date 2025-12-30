using CosmeticsStore.Application.Carts.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.RemoveItem
{
    public class RemoveItemCommand : IRequest<CartResponse>
    {
        public Guid CartId { get; }
        public Guid ItemId { get; }

        public RemoveItemCommand(Guid cartId, Guid itemId)
        {
            CartId = cartId;
            ItemId = itemId;
        }
    }
}
