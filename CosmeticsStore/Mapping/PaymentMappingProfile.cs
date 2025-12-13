using AutoMapper;
using CosmeticsStore.Application.Payment.AddPayment;
using CosmeticsStore.Application.Payment.UpdatePayment;
using CosmeticsStore.Dtos.Payment;
using AppPaymentResponse = CosmeticsStore.Application.Payment.AddPayment.PaymentResponse;

namespace CosmeticsStore.Mapping
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            // AddPayment: DTO -> Command
            CreateMap<AddPaymentRequest, AddPaymentCommand>()
                .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.OrderId))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency))
                .ForMember(d => d.Provider, opt => opt.MapFrom(s => s.Provider))
                .ForMember(d => d.TransactionId, opt => opt.MapFrom(s => s.TransactionId))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

            // UpdatePayment: DTO -> Command (partial)
            CreateMap<UpdatePaymentRequest, UpdatePaymentCommand>()
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency))
                .ForMember(d => d.Provider, opt => opt.MapFrom(s => s.Provider))
                .ForMember(d => d.TransactionId, opt => opt.MapFrom(s => s.TransactionId))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

            // Application response -> API response DTO
            CreateMap<AppPaymentResponse, PaymentResponseDto>()
                .ForMember(d => d.PaymentId, opt => opt.MapFrom(s => s.PaymentId))
                .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.OrderId))
                .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Currency))
                .ForMember(d => d.Provider, opt => opt.MapFrom(s => s.Provider))
                .ForMember(d => d.TransactionId, opt => opt.MapFrom(s => s.TransactionId))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc));
        }
    }
}
