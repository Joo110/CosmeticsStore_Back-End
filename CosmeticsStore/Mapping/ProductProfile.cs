using AutoMapper;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.Product;
using static CosmeticsStore.Dtos.Product.ProductResponseDto;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductModel, ProductResponseDto>();

        CreateMap<ProductVariantModel, ProductVariantResponseDto>();

        CreateMap<MediaModel, MediaResponseDto>();
    }
}
