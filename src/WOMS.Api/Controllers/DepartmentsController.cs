using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Departments.Commands.CreateDepartment;
using WOMS.Application.Features.Departments.Commands.UpdateDepartment;
using WOMS.Application.Features.Departments.Commands.DeleteDepartment;
using WOMS.Application.Features.Departments.DTOs;
using WOMS.Application.Features.Departments.Queries.GetDepartmentById;
using WOMS.Application.Features.Departments.Queries.GetAllDepartments;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : BaseController
    {
        private readonly IMediator _mediator;
        
        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new CreateDepartmentCommand
            {
                Name = createDepartmentDto.Name,
                Description = createDepartmentDto.Description,
                Code = createDepartmentDto.Code,
                Status = createDepartmentDto.Status,
                IsActive = createDepartmentDto.IsActive,
                CreatedBy = userIdClaim
            };

           
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetDepartment), new { id = result.Id }, result);
    
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DepartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(Guid id)
        {
            var query = new GetDepartmentByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DepartmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new UpdateDepartmentCommand
            {
                Id = id,
                Name = updateDepartmentDto.Name,
                Description = updateDepartmentDto.Description,
                Code = updateDepartmentDto.Code,
                Status = updateDepartmentDto.Status,
                IsActive = updateDepartmentDto.IsActive,
                UpdatedBy = userIdClaim
            };


                var result = await _mediator.Send(command);
                return Ok(result);
           
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteDepartment(Guid id)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new DeleteDepartmentCommand
            {
                Id = id,
                DeletedBy = userIdClaim
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
