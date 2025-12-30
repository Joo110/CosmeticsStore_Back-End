using AutoMapper;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

namespace CosmeticsStore.Application.Carts.GetById
{
    public class GetCartByIdQueryHandler : IRequestHandler<GetCartByIdQuery, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IMapper _mapper;

        public GetCartByIdQueryHandler(
            ICartRepository cartRepository,
            IProductVariantRepository productVariantRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _productVariantRepository = productVariantRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken)
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
