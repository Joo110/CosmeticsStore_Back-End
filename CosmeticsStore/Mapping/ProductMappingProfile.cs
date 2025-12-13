using AutoMapper;
using CosmeticsStore.Application.Product.AddProduct;
using CosmeticsStore.Application.Product.UpdateProduct;
using CosmeticsStore.Dtos.Product;
using AppProductResponse = CosmeticsStore.Application.Product.AddProduct.ProductResponse;

namespace CosmeticsStore.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // AddProductRequest -> AddProductCommand (including nested types)
            CreateMap<AddProductRequest, AddProductCommand>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Slug, opt => opt.MapFrom(s => s.Slug))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                .ForMember(d => d.IsPublished, opt => opt.MapFrom(s => s.IsPublished))
                .ForMember(d => d.Variants, opt => opt.MapFrom(s => s.Variants))
                .ForMember(d => d.Media, opt => opt.MapFrom(s => s.Media));

            CreateMap<AddProductRequest.CreateVariantDto, AddProductCommand.CreateVariantDto>();
            CreateMap<AddProductRequest.CreateMediaDto, AddProductCommand.CreateMediaDto>();

            // UpdateProductRequest -> UpdateProductCommand
            CreateMap<UpdateProductRequest, UpdateProductCommand>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Slug, opt => opt.MapFrom(s => s.Slug))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                .ForMember(d => d.IsPublished, opt => opt.MapFrom(s => s.IsPublished))
                .ForMember(d => d.Variants, opt => opt.MapFrom(s => s.Variants))
                .ForMember(d => d.Media, opt => opt.MapFrom(s => s.Media));

            CreateMap<UpdateProductRequest.UpdateVariantDto, UpdateProductCommand.UpdateVariantDto>();
            CreateMap<UpdateProductRequest.UpdateMediaDto, UpdateProductCommand.UpdateMediaDto>();

            // Application.ProductResponse -> ProductResponseDto
            CreateMap<AppProductResponse, ProductResponseDto>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Slug, opt => opt.MapFrom(s => s.Slug))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId))
                .ForMember(d => d.IsPublished, opt => opt.MapFrom(s => s.IsPublished))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc))
                .ForMember(d => d.Variants, opt => opt.MapFrom(s => s.Variants))
                .ForMember(d => d.Media, opt => opt.MapFrom(s => s.Media));

            CreateMap<AppProductResponse.ProductVariantResponse, ProductResponseDto.ProductVariantResponseDto>()
                .ForMember(d => d.ProductVariantId, opt => opt.MapFrom(s => s.ProductVariantId))
                .ForMember(d => d.Sku, opt => opt.MapFrom(s => s.Sku))
                .ForMember(d => d.PriceAmount, opt => opt.MapFrom(s => s.PriceAmount))
                .ForMember(d => d.PriceCurrency, opt => opt.MapFrom(s => s.PriceCurrency))
                .ForMember(d => d.Stock, opt => opt.MapFrom(s => s.Stock))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive));

            CreateMap<AppProductResponse.MediaResponse, ProductResponseDto.MediaResponseDto>()
                .ForMember(d => d.MediaId, opt => opt.MapFrom(s => s.MediaId))
                .ForMember(d => d.Url, opt => opt.MapFrom(s => s.Url))
                .ForMember(d => d.FileName, opt => opt.MapFrom(s => s.FileName))
                .ForMember(d => d.ContentType, opt => opt.MapFrom(s => s.ContentType))
                .ForMember(d => d.SizeInBytes, opt => opt.MapFrom(s => s.SizeInBytes))
                .ForMember(d => d.IsPrimary, opt => opt.MapFrom(s => s.IsPrimary))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc));
        }
    }
}
