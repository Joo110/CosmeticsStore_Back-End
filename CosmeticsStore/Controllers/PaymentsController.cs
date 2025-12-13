using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Payment.AddPayment;
using CosmeticsStore.Application.Payment.DeletePayment;
using CosmeticsStore.Application.Payment.GetAllPayments;
using CosmeticsStore.Application.Payment.GetPaymentById;
using CosmeticsStore.Application.Payment.UpdatePayment;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.Payment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class PaymentsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>Create a payment</summary>
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] AddPaymentRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddPaymentCommand>(request);
        var created = await mediator.Send(command, cancellationToken); // returns Application.Payment.AddPayment.PaymentResponse
        var dto = mapper.Map<PaymentResponseDto>(created);

        return CreatedAtAction(nameof(GetPaymentById), new { id = dto.PaymentId }, dto);
    }

    /// <summary>Get paginated payments (management)</summary>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<PaymentResponseDto>>> GetAllPayments(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? orderId = null,
        [FromQuery] string? status = null,
        [FromQuery] string? provider = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllPaymentsQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            OrderId = orderId,
            Status = status,
            Provider = provider
        };

        var paged = await mediator.Send(query, cancellationToken); // PaginatedList<Application.Payment.AddPayment.PaymentResponse>

        var itemsDto = mapper.Map<List<PaymentResponseDto>>(paged.Items);

        var result = new PaginatedList<PaymentResponseDto>(
            itemsDto,
            paged.TotalCount,
            paged.PageIndex,
            pageSize // pass pageSize to match your PaginatedList ctor
        );

        return Ok(result);
    }

    /// <summary>Get payment by id</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PaymentResponseDto>> GetPaymentById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetPaymentByIdQuery { PaymentId = id };
        var result = await mediator.Send(query, cancellationToken);

        var dto = mapper.Map<PaymentResponseDto>(result);
        return Ok(dto);
    }

    /// <summary>Update payment (partial)</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] UpdatePaymentRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdatePaymentCommand>(request);
        command.PaymentId = id;

        var updated = await mediator.Send(command, cancellationToken);
        var dto = mapper.Map<PaymentResponseDto>(updated);

        // Option A: return updated DTO
        return Ok(dto);

        // Option B: follow NoContent pattern:
        // return NoContent();
    }

    /// <summary>Delete payment</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePayment(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeletePaymentCommand { PaymentId = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
