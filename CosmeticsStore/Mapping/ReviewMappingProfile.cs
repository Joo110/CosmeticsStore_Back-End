using AutoMapper;
using CosmeticsStore.Application.Review.AddReview;
using CosmeticsStore.Application.Review.UpdateReview;
using CosmeticsStore.Dtos.Review;
using AppReviewResponse = CosmeticsStore.Application.Review.AddReview.ReviewResponse;

namespace CosmeticsStore.Mapping
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            // AddReview: DTO -> Command
            CreateMap<AddReviewRequest, AddReviewCommand>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Rating))
                .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Comment));

            // UpdateReview: DTO -> Command
            CreateMap<UpdateReviewRequest, UpdateReviewCommand>()
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Rating))
                .ForMember(d => d.Content, opt => opt.MapFrom(s => s.Comment));

            // Application response -> API DTO
            CreateMap<AppReviewResponse, ReviewResponseDto>()
                .ForMember(d => d.ReviewId, opt => opt.MapFrom(s => s.ReviewId))
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Rating))
                .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Content))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc));
        }
    }
}
