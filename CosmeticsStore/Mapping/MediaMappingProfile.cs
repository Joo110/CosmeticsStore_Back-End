using AutoMapper;
using CosmeticsStore.Application.Media;
using CosmeticsStore.Application.Media.AddMedia;
using CosmeticsStore.Dtos.Media;
using AppMediaResponse = CosmeticsStore.Application.Media.AddMedia.MediaResponse;

namespace CosmeticsStore.Mapping
{
    public class MediaMappingProfile : Profile
    {
        public MediaMappingProfile()
        {
            // Map request DTO -> AddMediaCommand (Object initializer)
            CreateMap<AddMediaItemRequest, AddMediaCommand>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.MediaType))
                .ForMember(dest => dest.SizeInBytes, opt => opt.MapFrom(src => src.Size));

            // Map application response -> API response DTO
            CreateMap<AppMediaResponse, MediaItemResponseDto>()
                .ForMember(d => d.MediaItemId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FileName, opt => opt.MapFrom(s => s.FileName))
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.Url))
                .ForMember(d => d.MediaType, opt => opt.MapFrom(s => s.ContentType))
                .ForMember(d => d.Size, opt => opt.MapFrom(s => s.SizeInBytes))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc));
        }
    }
}
