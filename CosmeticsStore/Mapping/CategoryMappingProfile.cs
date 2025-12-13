using AutoMapper;
using CosmeticsStore.Dtos.Category;
using CosmeticsStore.Application.Category.AddCategory;
using CosmeticsStore.Application.Category.UpdateCategory;

namespace CosmeticsStore.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryCreateRequest, AddCategoryCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryId))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            CreateMap<CategoryUpdateRequest, UpdateCategoryCommand>()
                .ConstructUsing(src => new UpdateCategoryCommand(
                    Guid.Empty,
                    src.Name,
                    src.Description,
                    src.ParentCategoryId,
                    src.IsActive
                ));
        }
    }
}
