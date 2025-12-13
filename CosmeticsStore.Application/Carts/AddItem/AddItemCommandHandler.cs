using AutoMapper;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Domain.Exceptions;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Application.Carts.AddItem
{
    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, CartResponse>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public AddItemCommandHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartResponse> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException("Cart not found for this user.");

            var existingItem = cart.Items.FirstOrDefault(i => i.Id == request.Item.ProductId);
            if (existingItem != null)
                existingItem.Quantity += request.Item.Quantity;
            else
                cart.Items.Add(new Domain.Entities.CartItem
                {
                    Id = request.Item.ProductId,
                    Quantity = request.Item.Quantity
                });

            await _cartRepository.UpdateAsync(cart, cancellationToken);
            return _mapper.Map<CartResponse>(cart);
        }
    }
}
