using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Coupon.AddCoupon;
using CosmeticsStore.Application.Coupon.DeleteCoupon;
using CosmeticsStore.Application.Coupon.GetAllCoupons;
using CosmeticsStore.Application.Coupon.GetCouponById;
using CosmeticsStore.Application.Coupon.UpdateCoupon;
using CosmeticsStore.Dtos.Coupont;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class CouponsController(ISender mediator, IMapper mapper) : ControllerBase
{
    // POST: Create Coupon
    [HttpPost]
    public async Task<IActionResult> CreateCoupon(CreateCouponRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddCouponCommand>(request);

        var couponId = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetCouponById), new { id = couponId }, null);
    }

    // GET: All Coupons
    [HttpGet]
    public async Task<IActionResult> GetAllCoupons(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCouponsQuery(), cancellationToken);

        var dto = mapper.Map<List<CouponResponseDto>>(result);

        return Ok(dto);
    }

    // GET: Coupon By Id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCouponById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCouponByIdQuery(id), cancellationToken);

        var dto = mapper.Map<CouponResponseDto>(result);

        return Ok(dto);
    }

    // PUT: Update Coupon
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCoupon(Guid id, UpdateCouponRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateCouponCommand>(request);
        command.CouponId = id;

        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    // DELETE: Delete Coupon
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCoupon(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCouponCommand(id), cancellationToken);

        return NoContent();
    }
}
