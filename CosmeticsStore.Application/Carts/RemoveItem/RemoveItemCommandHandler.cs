using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

namespace CosmeticsStore.Application.Carts.RemoveItem
{
    public class RemoveItemCommandHandler
        : IRequestHandler<RemoveItemCommand, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public RemoveItemCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> Handle(RemoveItemCommand request, CancellationToken cancellationToken)
        {
            await _cartRepository.RemoveItemAsync(request.CartId, request.ItemId, cancellationToken);

            var updatedCart = await _cartRepository.GetByIdAsync(request.CartId, cancellationToken);

            return _mapper.Map<CartResponse>(updatedCart);
        }
    }

}
