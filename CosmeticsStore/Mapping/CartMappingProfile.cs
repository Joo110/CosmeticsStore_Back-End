using AutoMapper;
using CosmeticsStore.Dtos.Cart;
using CreateCartItemDto = CosmeticsStore.Application.Carts.Create.CartItemDto;
using AddItemCartItemDto = CosmeticsStore.Application.Carts.AddItem.CartItemDto;

public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        // CartItemRequest -> Create.CartItemDto
        CreateMap<CartItemRequest, CreateCartItemDto>();

        // CartItemRequest -> AddItem.CartItemDto
        CreateMap<CartItemRequest, AddItemCartItemDto>();
    }
}
