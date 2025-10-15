using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.WorkflowStatus.Commands.CreateWorkflowStatus;
using WOMS.Application.Features.WorkflowStatus.Commands.DeleteWorkflowStatus;
using WOMS.Application.Features.WorkflowStatus.Commands.UpdateWorkflowStatus;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Application.Features.WorkflowStatus.Queries.GetAllWorkflowStatuses;
using WOMS.Application.Features.WorkflowStatus.Queries.GetWorkflowStatusById;
using WOMS.Domain.Enums;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkflowStatusController : BaseController
    {
        private readonly IMediator _mediator;

        public WorkflowStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkflowStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<WorkflowStatusDto>>> GetAllWorkflowStatuses(
            [FromQuery] bool activeOnly = true)
        {
            var query = new GetAllWorkflowStatusesQuery { ActiveOnly = activeOnly };
            var result = await _mediator.Send(query);

            return HandleResponse(StatusCodes.Status200OK, "Workflow statuses retrieved successfully", true, result, null);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkflowStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowStatusDto>> GetWorkflowStatusById(Guid id)
        {
            var query = new GetWorkflowStatusByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return HandleResponse(StatusCodes.Status200OK, "Workflow status retrieved successfully", true, result, null);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WorkflowStatusDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowStatusDto>> CreateWorkflowStatus([FromBody] CreateWorkflowStatusRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateWorkflowStatusCommand
            {
                Name = request.Name,
                Description = request.Description,
                Color = request.Color,
                Order = request.Order,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status201Created, "Workflow status created successfully", true, result, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WorkflowStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowStatusDto>> UpdateWorkflowStatus(Guid id, [FromBody] UpdateWorkflowStatusRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateWorkflowStatusCommand
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Color = request.Color,
                Order = request.Order,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status200OK, "Workflow status updated successfully", true, result, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteWorkflowStatus(Guid id)
        {
            var command = new DeleteWorkflowStatusCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
