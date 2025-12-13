using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;

namespace CosmeticsStore.Application.Carts.RemoveItem
{
    public class RemoveItemCommandHandler : IRequestHandler<RemoveItemCommand, CartResponse>
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
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException("Cart not found for this user.");

            var item = cart.Items.FirstOrDefault(i => i.Id == request.ProductId);
            if (item != null)
            {
                cart.Items.Remove(item);
                await _cartRepository.UpdateAsync(cart, cancellationToken);
            }

            return _mapper.Map<CartResponse>(cart);
        }
    }
}
