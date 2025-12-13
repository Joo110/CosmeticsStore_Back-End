using AutoMapper;
using CosmeticsStore.Application.Inventory.AddInventoryItem;
using CosmeticsStore.Application.Inventory.UpdateInventoryItem;
using CosmeticsStore.Dtos.Inventorys;
using AppInventoryResponse = CosmeticsStore.Application.Inventory.AddInventoryItem.InventoryItemResponse;

namespace CosmeticsStore.Mapping
{
    public class InventoryMappingProfile : Profile
    {
        public InventoryMappingProfile()
        {
            // Map request DTO -> AddInventoryItemCommand (construct using ctor)
            CreateMap<AddInventoryItemRequest, AddInventoryItemCommand>()
                .ConstructUsing(src => new AddInventoryItemCommand(src.ProductId, src.Quantity));

            // Map application response -> API response DTO
            CreateMap<AppInventoryResponse, InventoryItemResponseDto>()
                .ForMember(d => d.InventoryItemId, opt => opt.MapFrom(s => s.InventoryItemId))
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.ProductId))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(s => s.Quantity))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Location))
                .ForMember(d => d.CreatedAtUtc, opt => opt.MapFrom(s => s.CreatedAtUtc))
                .ForMember(d => d.ModifiedAtUtc, opt => opt.MapFrom(s => s.ModifiedAtUtc));

            // NOTE: UpdateInventoryItemCommand needs the id (route) so we create it manually in controller.
        }
    }
}
