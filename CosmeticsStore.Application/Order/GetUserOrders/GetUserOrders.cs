using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.GetUserOrders
{
    public class GetUserOrdersQuery : IRequest<List<OrderModel>>
    {
        public Guid UserId { get; set; }
    }
}
