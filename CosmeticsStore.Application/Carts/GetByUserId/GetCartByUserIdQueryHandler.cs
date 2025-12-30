using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

namespace CosmeticsStore.Application.Carts.GetByUserId
{
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;

        public GetCartByUserIdQueryHandler(
            ICartRepository cartRepository,
            IProductVariantRepository productVariantRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken)
                ?? throw new InvalidOperationException("Cart not found");

            foreach (var item in cart.Items)
            {
                var productName = await _productVariantRepository
                    .GetProductNameByVariantIdAsync(item.ProductVariantId, cancellationToken);
                item.Title = productName ?? item.Title;
            }

            return _mapper.Map<CartResponse>(cart);
        }
    }
}
