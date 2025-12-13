using AutoMapper;
using CosmeticsStore.Application.Coupon.AddCoupon;
using CosmeticsStore.Application.Coupon.UpdateCoupon;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.Coupont;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        // Request → Commands
        CreateMap<CreateCouponRequest, AddCouponCommand>();
        CreateMap<UpdateCouponRequest, UpdateCouponCommand>();

        // Query Result → Response DTO
        CreateMap<CouponModel, CouponResponseDto>();
    }
}
