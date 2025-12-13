using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.Product.AddProduct;
using CosmeticsStore.Application.Product.DeleteProduct;
using CosmeticsStore.Application.Product.GetAllProducts;
using CosmeticsStore.Application.Product.GetProductById;
using CosmeticsStore.Application.Product.UpdateProduct;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.Product;
using CosmeticsStore.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CosmeticsStore.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
//[Authorize(Roles = "Admin")]
public class ProductsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>Add product (with optional variants & media)</summary>
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] AddProductRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddProductCommand>(request);
        var created = await mediator.Send(command, cancellationToken); // returns Application.Product.AddProduct.ProductResponse
        var dto = mapper.Map<ProductResponseDto>(created);

        return CreatedAtAction(nameof(GetProductById), new { id = dto.ProductId }, dto);
    }

    /// <summary>Get paginated products for management</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAllProducts(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] bool? isPublished = null,
        [FromQuery] string? searchTerm = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllProductsQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            CategoryId = categoryId,
            IsPublished = isPublished,
            SearchTerm = searchTerm
        };

        var paged = await mediator.Send(query, cancellationToken); // PaginatedList<Application.Product.AddProduct.ProductResponse>

        // map items
        var itemsDto = mapper.Map<List<ProductResponseDto>>(paged.Items);

        // add pagination header (same pattern as HotelsController)
        var metadata = new
        {
            paged.TotalCount,
            paged.PageIndex,
            pageSize
        };
        Response.Headers.AddPaginationMetadata(metadata);

        return Ok(itemsDto);
    }

    /// <summary>Get product by id</summary>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProductResponseDto>> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery { ProductId = id };
        var result = await mediator.Send(query, cancellationToken);

        var dto = mapper.Map<ProductResponseDto>(result);
        return Ok(dto);
    }

    /// <summary>Update product (partial)</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateProductCommand>(request);
        command.ProductId = id;

        var updated = await mediator.Send(command, cancellationToken);
        var dto = mapper.Map<ProductResponseDto>(updated);

        return Ok(dto);
    }

    /// <summary>Delete product</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand { ProductId = id };
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
