using CosmeticsStore.Application.Order.AddOrder;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new OrderNotFoundException($"Order with id {request.OrderId} not found.");

            return new OrderResponse
            {
                OrderId = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                ShippingAddress = order.ShippingAddress,
                PhoneNumber = order.PhoneNumber,
                TotalAmount = order.TotalAmount,
                TotalCurrency = order.TotalCurrency,
                CreatedAtUtc = order.CreatedAtUtc,
                ModifiedAtUtc = order.ModifiedAtUtc,
                Items = order.Items?.Select(i => new OrderResponse.OrderItemResponse
                {
                    OrderItemId = i.Id,
                    ProductVariantId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPriceAmount,
                    Currency = i.UnitPriceCurrency
                }).ToList()
            };
        }
    }
}
