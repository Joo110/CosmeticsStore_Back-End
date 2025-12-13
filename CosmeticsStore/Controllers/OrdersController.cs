using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Order.AddOrder;
using CosmeticsStore.Application.Order.DeleteOrder;
using CosmeticsStore.Application.Order.GetAllOrders;
using CosmeticsStore.Application.Order.GetAllOrders.CosmeticsStore.Application.Order.GetAllOrders;
using CosmeticsStore.Application.Order.GetOrderById;
using CosmeticsStore.Application.Order.UpdateOrder;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OrdersController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>Create new order</summary>
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] AddOrderRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddOrderCommand>(request);
        var created = await mediator.Send(command, cancellationToken);
        var dto = mapper.Map<OrderResponseDto>(created);

        return CreatedAtAction(nameof(GetOrderById), new { id = dto.OrderId }, dto);
    }

    /// <summary>Get paginated orders (management)</summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<OrderResponseDto>>> GetAllOrders(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? userId = null,
        [FromQuery] string? status = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllOrdersQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            UserId = userId,
            Status = status
        };

        var paged = await mediator.Send(query, cancellationToken); // PaginatedList<Application.OrderResponse>

        var itemsDto = mapper.Map<List<OrderResponseDto>>(paged.Items);

        var result = new PaginatedList<OrderResponseDto>(
            itemsDto,
            paged.TotalCount,
            paged.PageIndex,
            pageSize
        );

        return Ok(result);
    }

    /// <summary>Get order by id</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponseDto>> GetOrderById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery { OrderId = id };
        var result = await mediator.Send(query, cancellationToken);

        var dto = mapper.Map<OrderResponseDto>(result);
        return Ok(dto);
    }

    /// <summary>Update order (partial/full)</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateOrderCommand>(request);
        command.OrderId = id;

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    /// <summary>Delete order</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrderCommand { OrderId = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
