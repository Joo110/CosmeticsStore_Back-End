using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Category.AddCategory;
using CosmeticsStore.Application.Category.DeleteCategory;
using CosmeticsStore.Application.Category.GetAllCategories;
using CosmeticsStore.Application.Category.GetCategoryById;
using CosmeticsStore.Application.Category.UpdateCategory;
using CosmeticsStore.Dtos.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
//[Authorize(Roles = "Admin")]
public class CategoriesController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>Add a new category</summary>
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequest request, CancellationToken cancellationToken)
    {
        // map request DTO -> AddCategoryCommand
        var command = mapper.Map<AddCategoryCommand>(request);

        var categoryId = await mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, null);
    }

    /// <summary>Get all categories</summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<CategoryResponseDto>>> GetAllCategories(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllCategoriesQuery(), cancellationToken);

        // map query result -> response DTO list
        var mapped = mapper.Map<List<CategoryResponseDto>>(result);

        return Ok(mapped);
    }

    /// <summary>Get category by id</summary>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<CategoryResponseDto>> GetCategoryById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);

        var mapped = mapper.Map<CategoryResponseDto>(result);

        return Ok(mapped);
    }

    /// <summary>Update category</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateCategoryCommand>(request);

        try
        {
            // إذا property موجودة:
            var idProp = command.GetType().GetProperty("CategoryId");
            if (idProp != null && idProp.CanWrite)
            {
                idProp.SetValue(command, id);
            }
        }
        catch
        {
        }

        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>Delete category</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

        return NoContent();
    }
}
