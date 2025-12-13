using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Media.AddMedia;
using CosmeticsStore.Application.Media.DeleteMedia;
using CosmeticsStore.Application.Media.GetMediaById;
using CosmeticsStore.Application.Media.UpdateMedia;
using CosmeticsStore.Dtos.Media;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class MediaController(ISender mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddMediaItem([FromBody] AddMediaItemRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddMediaCommand>(request);
        var createdId = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetMediaItemById), new { id = createdId }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMediaItemById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMediaByIdQuery
        {
            MediaId = id
        };

        var result = await mediator.Send(query, cancellationToken);
        var dto = mapper.Map<MediaItemResponseDto>(result);

        return Ok(dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMediaItem(Guid id, [FromBody] UpdateMediaItemRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateMediaCommand>(request);

        // set the id property
        command.MediaId = id;

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMediaItem(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteMediaCommand
        {
            MediaId = id
        };

        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
