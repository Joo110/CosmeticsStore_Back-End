using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Order.AddOrder
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public AddOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new CosmeticsStore.Domain.Entities.Order
            {
                UserId = request.UserId,
                ShippingAddress = request.ShippingAddress,
                PhoneNumber = request.PhoneNumber,
                Status = request.Status,
                TotalAmount = request.TotalAmount,
                TotalCurrency = request.TotalCurrency,
                CreatedAtUtc = DateTime.UtcNow
            };

            if (request.Items != null && request.Items.Any())
            {
                foreach (var it in request.Items)
                {
                    order.Items.Add(new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductVariantId = it.ProductVariantId,
                        Quantity = it.Quantity,
                        UnitPriceAmount = it.UnitPrice,
                        UnitPriceCurrency = it.Currency
                    });
                }
            }

            var created = await _orderRepository.CreateAsync(order, cancellationToken);
            return new OrderResponse
            {
                OrderId = created.Id,
                UserId = created.UserId,
                Status = created.Status,
                ShippingAddress = request.ShippingAddress,
                PhoneNumber = request.PhoneNumber,
                TotalAmount = created.TotalAmount,
                TotalCurrency = created.TotalCurrency,
                CreatedAtUtc = created.CreatedAtUtc,
                ModifiedAtUtc = created.ModifiedAtUtc,
                Items = created.Items?.Select(i => new OrderResponse.OrderItemResponse
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
