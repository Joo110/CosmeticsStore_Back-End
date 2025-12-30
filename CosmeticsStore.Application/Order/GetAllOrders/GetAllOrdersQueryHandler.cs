using CosmeticsStore.Application.Order;
using CosmeticsStore.Application.Order.AddOrder;
using CosmeticsStore.Application.Order.GetAllOrders.CosmeticsStore.Application.Order.GetAllOrders;
using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Domain.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginatedList<OrderResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<PaginatedList<OrderResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = new Query<Domain.Entities.Order>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var paged = await _orderRepository.GetForManagementAsync(query, cancellationToken);

            var items = paged.Items.Select(m => new OrderResponse
            {
                OrderId = m.Id,
                UserId = m.UserId,
                Status = m.Status,
                ShippingAddress = m.ShippingAddress,
                PhoneNumber = m.PhoneNumber,
                TotalAmount = m.TotalAmount,
                TotalCurrency = m.TotalCurrency,
                CreatedAtUtc = m.CreatedAtUtc,
                ModifiedAtUtc = m.ModifiedAtUtc,
                Items = m.Items?.Select(i => new OrderResponse.OrderItemResponse
                {
                    OrderItemId = i.Id,
                    ProductVariantId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPriceAmount,
                    Currency = i.UnitPriceCurrency
                }).ToList()
            }).ToList();

            var result = new PaginatedList<OrderResponse>(
                items,
                paged.TotalCount,
                paged.PageIndex,
                request.PageSize
            );

            return result;
        }
    }
}
