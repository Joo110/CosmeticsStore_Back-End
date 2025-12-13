using CosmeticsStore.Application.Order.AddOrder;
using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                throw new OrderNotFoundException($"Order with id {request.OrderId} not found.");

            if (request.Status != null) order.Status = request.Status;
            if (request.ShippingAddressId.HasValue) order.ShippingAddressId = request.ShippingAddressId;
            if (request.TotalAmount.HasValue) order.TotalAmount = request.TotalAmount.Value;
            if (request.TotalCurrency != null) order.TotalCurrency = request.TotalCurrency;

            // If items provided: simple replacement strategy (delete existing items, add new ones).
            if (request.Items != null)
            {
                order.Items.Clear();
                foreach (var it in request.Items)
                {
                    order.Items.Add(new OrderItem
                    {
                        // If you want to preserve item Ids when provided:
                        // Id = it.OrderItemId ?? Guid.NewGuid(),
                        Id = it.ProductId,
                        Quantity = it.Quantity,
                        UnitPriceAmount = it.UnitPrice,
                        UnitPriceCurrency = it.Currency
                    });
                }
            }

            order.ModifiedAtUtc = DateTime.UtcNow;

            await _orderRepository.UpdateAsync(order, cancellationToken);

            return new OrderResponse
            {
                OrderId = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                ShippingAddressId = order.ShippingAddressId,
                TotalAmount = order.TotalAmount,
                TotalCurrency = order.TotalCurrency,
                CreatedAtUtc = order.CreatedAtUtc,
                ModifiedAtUtc = order.ModifiedAtUtc,
                Items = order.Items?.Select(i => new OrderResponse.OrderItemResponse
                {
                    OrderItemId = i.Id,
                    ProductId = i.ProductVariantId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPriceAmount,
                    Currency = i.UnitPriceCurrency
                }).ToList()
            };
        }
    }
}
