using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }
}
