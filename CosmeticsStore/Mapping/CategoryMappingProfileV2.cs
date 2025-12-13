using AutoMapper;
using CosmeticsStore.Application.Category.AddCategory;
using CosmeticsStore.Dtos.Category;
using CosmeticsStore.Application.Category.UpdateCategory;

namespace CosmeticsStore.Mapping
{
    public class CategoryMappingProfileV2 : Profile
    {
        public CategoryMappingProfileV2()
        {
            // Map requests -> commands
            CreateMap<CategoryCreateRequest, AddCategoryCommand>();
            CreateMap<CategoryUpdateRequest, UpdateCategoryCommand>()
                .ConstructUsing(src => new UpdateCategoryCommand(
                    Guid.Empty,
                    src.Name,
                    src.Description,
                    src.ParentCategoryId,
                    src.IsActive
                ));

            // Map responses -> DTOs
            CreateMap<CategoryResponse, CategoryResponseDto>();
        }
    }
}
