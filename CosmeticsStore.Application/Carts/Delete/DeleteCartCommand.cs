using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.Delete
{
    public class DeleteCartCommand : IRequest<Unit>
    {
        public Guid UserId { get; }

        public DeleteCartCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
