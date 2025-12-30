using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, List<OrderModel>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetUserOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderModel>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetUserOrdersAsync(request.UserId, cancellationToken);
        }
    }
}
