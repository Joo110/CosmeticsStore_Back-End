using CosmeticsStore.Application.Order.AddOrder;
using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

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

            if (!string.IsNullOrWhiteSpace(request.Status))
                order.Status = request.Status;

            if (!string.IsNullOrWhiteSpace(request.ShippingAddress))
                order.ShippingAddress = request.ShippingAddress;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                order.PhoneNumber = request.PhoneNumber;

            if (request.TotalAmount.HasValue)
                order.TotalAmount = request.TotalAmount.Value;

            if (!string.IsNullOrWhiteSpace(request.TotalCurrency))
                order.TotalCurrency = request.TotalCurrency;

            if (request.Items != null)
            {
                order.Items.Clear();
                foreach (var it in request.Items)
                {
                    order.Items.Add(new OrderItem
                    {
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
