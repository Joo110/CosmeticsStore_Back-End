using CosmeticsStore.Application.Carts.GetById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.GetByUserId
{
    public class GetCartByUserIdQuery : IRequest<CartResponse>
    {
        public Guid UserId { get; }

        public GetCartByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
