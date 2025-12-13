using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Carts.AddItem;
using CosmeticsStore.Application.Carts.Create;
using CosmeticsStore.Application.Carts.Delete;
using CosmeticsStore.Application.Carts.GetById;
using CosmeticsStore.Application.Carts.GetByUserId;
using CosmeticsStore.Dtos.Cart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AddItemCartItemDto = CosmeticsStore.Application.Carts.AddItem.CartItemDto;
using CreateCartItemDto = CosmeticsStore.Application.Carts.Create.CartItemDto;

namespace CosmeticsStore.Controllers;


[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CartsController(ISender mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCart(CreateCartRequest request, CancellationToken cancellationToken)
    {
        var items = mapper.Map<List<CreateCartItemDto>>(request.Items ?? new List<CartItemRequest>());
        var command = new CreateCartCommand(request.UserId, items);

        var createdCart = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetCartById), new { id = createdCart.Id }, createdCart);
    }

    [HttpPost("{userId:guid}/items")]
    public async Task<IActionResult> AddItem(Guid userId, CartItemRequest request, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<AddItemCartItemDto>(request);
        var command = new AddItemCommand(userId, dto);

        var updatedCart = await mediator.Send(command, cancellationToken);

        return Ok(updatedCart);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteCart(Guid userId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCartCommand(userId), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCartById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCartByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetCartByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCartByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }
}
