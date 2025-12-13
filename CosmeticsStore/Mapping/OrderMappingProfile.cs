using AutoMapper;
using CosmeticsStore.Application.Order.AddOrder;
using CosmeticsStore.Application.Order.UpdateOrder;
using CosmeticsStore.Dtos.Order;
using AppOrderResponse = CosmeticsStore.Application.Order.AddOrder.OrderResponse;

namespace CosmeticsStore.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // AddOrder: map request DTO -> AddOrderCommand (map nested items too)
            CreateMap<AddOrderRequest, AddOrderCommand>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ShippingAddressId, opt => opt.MapFrom(src => src.ShippingAddressId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.TotalCurrency, opt => opt.MapFrom(src => src.TotalCurrency))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<AddOrderRequest.CreateOrderItemDto, AddOrderCommand.CreateOrderItemDto>();

            // UpdateOrder: map UpdateOrderRequest -> UpdateOrderCommand
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ShippingAddressId, opt => opt.MapFrom(src => src.ShippingAddressId))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.TotalCurrency, opt => opt.MapFrom(src => src.TotalCurrency))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<UpdateOrderRequest.UpdateOrderItemDto, UpdateOrderCommand.UpdateOrderItemDto>();

            // Application OrderResponse -> API OrderResponseDto
            CreateMap<AppOrderResponse, OrderResponseDto>()
                .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.OrderId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.ShippingAddressId, opt => opt.MapFrom(s => s.ShippingAddressId))
                .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.TotalAmount))
                .ForMember(d => d.TotalCurrency, opt => opt.MapFrom(s => s.TotalCurrency))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc))
                .ForMember(d => d.Items, opt => opt.MapFrom(s => s.Items))
                .ForMember(d => d.Payments, opt => opt.MapFrom(s => s.Payments));

            // Map nested response items/payments
            CreateMap<AppOrderResponse.OrderItemResponse, OrderResponseDto.OrderItemResponseDto>()
                .ForMember(d => d.OrderItemId, opt => opt.MapFrom(s => s.OrderItemId))
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ForMember(d => d.UnitPrice, opt => opt.MapFrom(s => s.UnitPrice))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency));

            CreateMap<AppOrderResponse.PaymentResponse, OrderResponseDto.PaymentResponseDto>()
                .ForMember(d => d.PaymentId, opt => opt.MapFrom(s => s.PaymentId))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency))
                .ForMember(d => d.Method, opt => opt.MapFrom(s => s.Method))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc));
        }
    }
}
