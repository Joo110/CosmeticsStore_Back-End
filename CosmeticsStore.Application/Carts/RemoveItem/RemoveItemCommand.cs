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
        public Guid UserId { get; }
        public Guid ProductId { get; }

        public RemoveItemCommand(Guid userId, Guid productId)
        {
            UserId = userId;
            ProductId = productId;
        }
    }
}
