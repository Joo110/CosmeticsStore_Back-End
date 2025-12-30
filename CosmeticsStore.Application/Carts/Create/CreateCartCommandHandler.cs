using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Entities;
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
            var userId = request.UserId; // or get from current user in controller
            if (userId == Guid.Empty)
                throw new InvalidOperationException("UserId is required.");

            var existing = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
            if (existing != null) throw new InvalidOperationException("User already has a cart.");

            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _cartRepository.CreateAsync(cart, cancellationToken);
            await _cartRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CartResponse>(cart);
        }
    }
}
