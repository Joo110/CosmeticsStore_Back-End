using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Inventory.AddInventoryItem;
using CosmeticsStore.Application.Inventory.DeleteInventoryItem;
using CosmeticsStore.Application.Inventory.GetInventoryItemById;
using CosmeticsStore.Application.Inventory.UpdateInventoryItem;
using CosmeticsStore.Dtos.Inventorys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class InventoryController(ISender mediator, IMapper mapper) : ControllerBase
{
    // POST: /api/inventory
    [HttpPost]
    public async Task<IActionResult> AddInventoryItem(AddInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var command = new AddInventoryItemCommand(request.ProductId, request.Quantity);

        var createdId = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetInventoryItemById), new { id = createdId }, null);
    }

    // GET: /api/inventory/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInventoryItemById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetInventoryItemByIdQuery(id), cancellationToken);

        var dto = mapper.Map<InventoryItemResponseDto>(result);

        return Ok(dto);
    }

    // PUT: /api/inventory/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateInventoryItem(Guid id, UpdateInventoryItemRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateInventoryItemCommand(id, request.Quantity, request.Location);

        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    // DELETE: /api/inventory/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteInventoryItem(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteInventoryItemCommand(id), cancellationToken);

        return NoContent();
    }
}
