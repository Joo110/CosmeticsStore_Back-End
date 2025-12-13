using Asp.Versioning;
using AutoMapper;
using CosmeticsStore.Application.User.AddUser;
using CosmeticsStore.Application.User.Auth;
using CosmeticsStore.Application.User.DeleteUser;
using CosmeticsStore.Application.User.GetAllUsers;
using CosmeticsStore.Application.User.GetUserById;
using CosmeticsStore.Application.User.UpdateUser;
using CosmeticsStore.Domain.Models;
using CosmeticsStore.Dtos.User;
using CosmeticsStore.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticsStore.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
public class UsersController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>Admin: create user</summary>
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] AddUserRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<AddUserCommand>(request);
        var created = await mediator.Send(command, cancellationToken); // returns UserResponse
        var dto = mapper.Map<UserResponseDto>(created);

        return CreatedAtAction(nameof(GetUserById), new { id = dto.UserId }, dto);
    }

    /// <summary>Register (public): create account + return token</summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<RegisterUserCommand>(request);
        var auth = await mediator.Send(command, cancellationToken); // returns AuthResponse
        var dto = mapper.Map<AuthResponseDto>(auth);

        return Ok(dto);
    }

    /// <summary>Login (public) -> token</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<LoginCommand>(request);
        var auth = await mediator.Send(command, cancellationToken);
        var dto = mapper.Map<AuthResponseDto>(auth);
        return Ok(dto);
    }

    /// <summary>Get paginated users (admin)</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? role = null,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllUsersQuery
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            Role = role
        };

        var paged = await mediator.Send(query, cancellationToken); // PaginatedList<Application.User.AddUser.UserResponse>

        var itemsDto = mapper.Map<List<UserResponseDto>>(paged.Items);

        // use request pageIndex/pageSize (safer if PaginatedList lacks PageSize prop)
        var metadata = new
        {
            paged.TotalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        Response.Headers.AddPaginationMetadata(metadata);

        return Ok(itemsDto);
    }

    /// <summary>Get user by id (admin) — could also allow self-service by altering policy</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { UserId = id };
        var result = await mediator.Send(query, cancellationToken);
        var dto = mapper.Map<UserResponseDto>(result);
        return Ok(dto);
    }

    /// <summary>Update user (admin). You can change to allow users update their own profile by policy.</summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateUserCommand>(request);
        command.UserId = id;

        var updated = await mediator.Send(command, cancellationToken);
        var dto = mapper.Map<UserResponseDto>(updated);

        return Ok(dto); // or NoContent()
    }

    /// <summary>Delete user (admin)</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand { UserId = id };
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
