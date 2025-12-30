using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Entities;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.AddItem
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;

        public AddItemCommandHandler(
            ICartRepository cartRepository,
            IProductVariantRepository productVariantRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            // 1. Get or Create Cart
            var cart = await _cartRepository.GetByUserIdForUpdateAsync(request.UserId, cancellationToken);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    CreatedAtUtc = DateTime.UtcNow,
                    ModifiedAtUtc = DateTime.UtcNow
                };
                await _cartRepository.CreateAsync(cart, cancellationToken);
                await _cartRepository.SaveChangesAsync(cancellationToken);
            }

            // 2. Validate Product Variant
            var variant = await _productVariantRepository.GetByIdAsync(request.ProductVariantId, cancellationToken);
            if (variant == null)
                throw new InvalidOperationException("Product variant does not exist");
            if (!variant.IsActive)
                throw new InvalidOperationException("Product variant is not active");
            if (variant.StockQuantity < request.Quantity)
                throw new InvalidOperationException("Not enough stock");

            // 3. Check if item already exists in cart
            var existingItem = cart.Items?.FirstOrDefault(i => i.ProductVariantId == variant.Id);

            if (existingItem != null)
            {
                // ⭐ Update existing item
                existingItem.Quantity += request.Quantity;
                existingItem.ModifiedAtUtc = DateTime.UtcNow;
            }
            else
            {
                // ⭐ Add NEW item using repository method
                var newItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductVariantId = variant.Id,
                    Quantity = request.Quantity,
                    UnitPriceAmount = variant.PriceAmount,
                    UnitPriceCurrency = variant.PriceCurrency,
                    Title = variant.Title,
                    CreatedAtUtc = DateTime.UtcNow,
                    ModifiedAtUtc = DateTime.UtcNow
                };

                await _cartRepository.AddItemAsync(newItem, cancellationToken);
            }

            cart.ModifiedAtUtc = DateTime.UtcNow;

            await _cartRepository.SaveChangesAsync(cancellationToken);

            cart = await _cartRepository.GetByUserIdForUpdateAsync(request.UserId, cancellationToken);

            return _mapper.Map<CartResponse>(cart);
        }
    }
}
