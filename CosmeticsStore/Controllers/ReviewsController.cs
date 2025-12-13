using AutoMapper;
using CosmeticsStore.Application.Review.AddReview;
using CosmeticsStore.Application.Review.DeleteReview;
using CosmeticsStore.Application.Review.GetAllReviews;
using CosmeticsStore.Application.Review.GetReviewById;
using CosmeticsStore.Application.Review.UpdateReview;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.Review;
using CosmeticsStore.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class ReviewsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>Create a review</summary>
    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] AddReviewRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddReviewCommand>(request);
        var created = await mediator.Send(command, cancellationToken); // returns application ReviewResponse
        var dto = mapper.Map<ReviewResponseDto>(created);

        return CreatedAtAction(nameof(GetReviewById), new { id = dto.ReviewId }, dto);
    }

    /// <summary>Get paginated reviews (management)</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetAllReviews(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? productId = null,
        [FromQuery] Guid? userId = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllReviewsQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            ProductId = productId,
            UserId = userId,
        };

        var paged = await mediator.Send(query, cancellationToken); // PaginatedList<Application.Review.AddReview.ReviewResponse>

        // map items
        var itemsDto = mapper.Map<List<ReviewResponseDto>>(paged.Items);

        // add pagination header using the request pageIndex/pageSize (safer if PaginatedList lacks these props)
        var metadata = new
        {
            paged.TotalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        Response.Headers.AddPaginationMetadata(metadata);

        return Ok(itemsDto);
    }

    /// <summary>Get review by id</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReviewResponseDto>> GetReviewById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetReviewByIdQuery { ReviewId = id };
        var result = await mediator.Send(query, cancellationToken);

        var dto = mapper.Map<ReviewResponseDto>(result);
        return Ok(dto);
    }

    /// <summary>Update review (partial)</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateReview(Guid id, [FromBody] UpdateReviewRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateReviewCommand>(request);
        command.ReviewId = id;

        var updated = await mediator.Send(command, cancellationToken);
        var dto = mapper.Map<ReviewResponseDto>(updated);

        return Ok(dto); // أو NoContent() لو تفضل
    }

    /// <summary>Delete review</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReview(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteReviewCommand { ReviewId = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
