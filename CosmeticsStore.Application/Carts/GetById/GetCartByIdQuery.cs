using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.GetById
{
    public class GetCartByIdQuery : IRequest<CartResponse>
    {
        public Guid CartId { get; }

        public GetCartByIdQuery(Guid cartId)
        {
            CartId = cartId;
        }
    }
}
