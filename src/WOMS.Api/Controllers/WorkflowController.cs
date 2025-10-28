using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Workflow.Commands.AddNode;
using WOMS.Application.Features.Workflow.Commands.ConnectNodes;
using WOMS.Application.Features.Workflow.Commands.CreateWorkflow;
using WOMS.Application.Features.Workflow.Commands.DeleteNode;
using WOMS.Application.Features.Workflow.Commands.DisconnectNodes;
using WOMS.Application.Features.Workflow.Commands.UpdateNode;
using WOMS.Application.Features.Workflow.Commands.ToggleWorkflowStatus;
using WOMS.Application.Features.Workflow.Commands.UpdateWorkflow;
using WOMS.Application.Features.Workflow.Commands.CreateWorkflowNotification;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Application.Features.Workflow.Queries.GetAllWorkflows;
using WOMS.Application.Features.Workflow.Queries.GetNodeTypes;
using WOMS.Application.Features.Workflow.Queries.GetWorkflowById;
using WOMS.Application.Features.Workflow.Queries.TestWorkflow;
using WOMS.Domain.Enums;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkflowController : BaseController
    {
        private readonly IMediator _mediator;

        public WorkflowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WorkflowListGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WorkflowListGetResponse>> GetAllWorkflows(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] WorkflowCategory? category = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true)
        {
            if (pageNumber < 1)
                return BadRequest("Page number must be greater than 0");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100");

            var query = new GetAllWorkflowsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                Category = category,
                IsActive = isActive,
                SortBy = sortBy,
                SortDescending = sortDescending
            };
            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Workflows retrieved successfully", true, result, null);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkflowGetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowGetDto>> GetWorkflow(Guid id)
        {
            var query = new GetWorkflowByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return HandleResponse(StatusCodes.Status200OK, "Workflows retrieved successfully", true, result, null);
        }


        [HttpPost]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowDto>> CreateWorkflow([FromBody] CreateWorkflowRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateWorkflowCommand
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Nodes = request.Nodes
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status201Created, "Workflow created successfully", true, result, null);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WorkflowDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowDto>> UpdateWorkflow(Guid id, [FromBody] UpdateWorkflowRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateWorkflowCommand
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                IsActive = request.IsActive,
                Nodes = request.Nodes
            };
            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status200OK, "Workflow updated successfully", true, result, null);
        }


        [HttpPost("nodes")]
        [ProducesResponseType(typeof(WorkflowNodeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowNodeDto>> AddNode([FromBody] AddNodeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var command = new AddNodeCommand
            {
                WorkflowId = request.WorkflowId,
                Type = request.Type,
                Title = request.Title,
                Description = request.Description,
                Position = request.Position,
                Data = request.Data,
                OrderIndex = request.OrderIndex
            };
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetWorkflow), new { id = request.WorkflowId }, result);
        }

        [HttpPut("nodes/{nodeId}")]
        [ProducesResponseType(typeof(WorkflowNodeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowNodeDto>> UpdateNode(Guid nodeId, [FromBody] UpdateNodeRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var command = new UpdateNodeCommand
            {
                NodeId = nodeId,
                Title = request.Title,
                Description = request.Description,
                Position = request.Position,
                Data = request.Data,
                OrderIndex = request.OrderIndex
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("nodes/{nodeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteNode(Guid nodeId)
        {
            var command = new DeleteNodeCommand { NodeId = nodeId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("nodes/connect")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ConnectNodes([FromBody] ConnectNodesRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new ConnectNodesCommand
            {
                FromNodeId = request.FromNodeId,
                ToNodeId = request.ToNodeId
            };
            var result = await _mediator.Send(command);
            return Ok(new { success = result });
        }

        [HttpPost("nodes/disconnect")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DisconnectNodes([FromBody] DisconnectNodesRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new DisconnectNodesCommand
            {
                FromNodeId = request.FromNodeId,
                ToNodeId = request.ToNodeId
            };
            var result = await _mediator.Send(command);
            return Ok(new { success = result });
        }

        [HttpGet("node-types")]
        [ProducesResponseType(typeof(List<NodeTypeInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<NodeTypeInfoDto>>> GetNodeTypes()
        {
            var query = new GetNodeTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}/toggle-status")]
        [ProducesResponseType(typeof(ToggleWorkflowStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ToggleWorkflowStatusResponse>> ToggleWorkflowStatus(Guid id, [FromBody] ToggleWorkflowStatusRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new ToggleWorkflowStatusCommand
            {
                WorkflowId = id,
                IsActive = request.IsActive
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status200OK, result.Message, true, result, null);

        }

        [HttpPost("test")]
        [ProducesResponseType(typeof(TestWorkflowResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TestWorkflowResponse>> TestWorkflow([FromBody] TestWorkflowRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var query = new TestWorkflowQuery
            {
                WorkflowId = request.WorkflowId,
                TestData = request.TestData
            };

            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Workflow test completed", true, result, null);
        }

        [HttpPost("notifications")]
        [ProducesResponseType(typeof(WorkflowNotificationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<WorkflowNotificationDto>> CreateWorkflowNotification([FromBody] CreateWorkflowNotificationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateWorkflowNotificationCommand
            {
                WorkflowId = request.WorkflowId,
                Name = request.Name,
                Type = request.Type,
                Recipients = request.Recipients,
                Template = request.Template,
                Triggers = request.Triggers,
                IsActive = request.IsActive,
                OrderIndex = request.OrderIndex
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetWorkflow), new { id = request.WorkflowId }, result);
        }
    }
}
