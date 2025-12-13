using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.GetAllOrders
{
    using global::CosmeticsStore.Application.Order.AddOrder;
    using global::CosmeticsStore.Domain.Models;
    using MediatR;
    using System;

    namespace CosmeticsStore.Application.Order.GetAllOrders
    {
        public class GetAllOrdersQuery : IRequest<PaginatedList<OrderResponse>>
        {
            public int PageIndex { get; set; } = 1;
            public int PageSize { get; set; } = 20;

            public Guid? UserId { get; set; } // filter by user
            public string? Status { get; set; } // filter by status
        }
    }

}
