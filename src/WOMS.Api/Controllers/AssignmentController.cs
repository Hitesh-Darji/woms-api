using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Assignment.Commands.AssignWorkOrder;
using WOMS.Application.Features.Assignment.Commands.AutoAssignAll;
using WOMS.Application.Features.Assignment.DTOs;
using WOMS.Application.Features.Assignment.Queries.GetAssignmentRecommendations;
using WOMS.Application.Features.Assignment.Queries.GetTechnicianStatus;
using WOMS.Application.Features.Assignment.Queries.GetUnassignedWorkOrders;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssignmentController : BaseController
    {
        private readonly IMediator _mediator;

        public AssignmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("UnassignedWorkOrders")]
        [ProducesResponseType(typeof(UnassignedWorkOrdersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UnassignedWorkOrdersResponse>> GetUnassignedWorkOrders(
            [FromQuery] string? priority = null,
            [FromQuery] string? workType = null,
            [FromQuery] string? location = null)
        {
            var query = new GetUnassignedWorkOrdersQuery
            {
                Priority = priority,
                WorkType = workType,
                Location = location
            };

            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Unassigned work orders retrieved successfully", true, result, null);
        }

        [HttpGet("TechnicianStatus")]
        [ProducesResponseType(typeof(TechnicianStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TechnicianStatusResponse>> GetTechnicianStatus(
            [FromQuery] string? status = null,
            [FromQuery] string? location = null)
        {
            var query = new GetTechnicianStatusQuery
            {
                Status = status,
                Location = location
            };

            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Technician status retrieved successfully", true, result, null);
        }

        [HttpPost("AssignWorkOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AssignWorkOrder([FromBody] AssignWorkOrderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                var command = new AssignWorkOrderCommand
                {
                    WorkOrderId = request.WorkOrderId,
                    TechnicianId = request.TechnicianId
                };

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Work order or technician not found.");

            return Ok(new { message = "Work order assigned successfully" });
        }

        [HttpPost("AutoAssignAll")]
        [ProducesResponseType(typeof(AutoAssignAllResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AutoAssignAllResponse>> AutoAssignAll([FromBody] AutoAssignAllRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AutoAssignAllCommand
            {
                ForceReassignment = request.ForceReassignment,
                PriorityFilter = request.PriorityFilter
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status200OK, "Auto-assignment completed successfully", true, result, null);
        }

        [HttpGet("WorkOrders/{workOrderId}/Recommendations")]
        [ProducesResponseType(typeof(AssignmentRecommendationsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AssignmentRecommendationsDto>> GetAssignmentRecommendations(Guid workOrderId)
        {
            var query = new GetAssignmentRecommendationsQuery { WorkOrderId = workOrderId };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound($"Work order with ID {workOrderId} not found.");

            return HandleResponse(StatusCodes.Status200OK, "Assignment recommendations retrieved successfully", true, result, null);
        }
    }
}
