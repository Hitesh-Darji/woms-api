using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Roles.Commands.CreateRole;
using WOMS.Application.Features.Roles.Commands.DeleteRole;
using WOMS.Application.Features.Roles.Commands.UpdateRole;
using WOMS.Application.Features.Roles.DTOs;
using WOMS.Application.Features.Roles.Queries.GetAllRoles;
using WOMS.Application.Features.Roles.Queries.GetRoleById;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : BaseController
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto createRoleDto)
        {
            Console.WriteLine("CreateRole endpoint called");
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new CreateRoleCommand
            {
                Name = createRoleDto.Name,
                Description = createRoleDto.Description,
                CreatedBy = userIdClaim
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRole), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RoleDto>> GetRole(string id)
        {
            var query = new GetRoleByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RoleDto>> UpdateRole(string id, [FromBody] UpdateRoleDto updateRoleDto)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new UpdateRoleCommand
            {
                Id = id,
                Name = updateRoleDto.Name,
                Description = updateRoleDto.Description,
                UpdatedBy = userIdClaim
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var command = new DeleteRoleCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
