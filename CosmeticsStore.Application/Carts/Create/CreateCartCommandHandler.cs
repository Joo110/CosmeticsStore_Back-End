using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

namespace CosmeticsStore.Application.Carts.Create
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CreateCartCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var existingCart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (existingCart != null)
            {
                throw new ("User already has a cart.");
            }

            var newCart = new Domain.Entities.Cart
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Items = request.Items.Select(i => new Domain.Entities.CartItem
                {
                    ProductVariantId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            await _cartRepository.CreateAsync(newCart, cancellationToken);

            return _mapper.Map<CartResponse>(newCart);
        }
    }
}
