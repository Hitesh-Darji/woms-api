using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.WorkOrder.DTOs;
using WOMS.Application.Features.WorkOrder.Queries.GetAllWorkOrders;
using WOMS.Application.Features.WorkOrder.Queries.GetWorkOrderViewList;
using WOMS.Domain.Enums;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkOrdersController : BaseController
    {
        private readonly IMediator _mediator;

        public WorkOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WorkOrderListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WorkOrderListResponse>> GetAllWorkOrders(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? status = null,
            [FromQuery] string? priority = null,
            [FromQuery] string? assignedTechnicianId = null,
            [FromQuery] Guid? workOrderTypeId = null,
            [FromQuery] DateTime? scheduledDateFrom = null,
            [FromQuery] DateTime? scheduledDateTo = null,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool sortDescending = true)
        {
            if (pageNumber < 1)
                return BadRequest("Page number must be greater than 0");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100");

            var query = new GetAllWorkOrdersQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                Status = !string.IsNullOrEmpty(status) && Enum.TryParse<WorkOrderStatus>(status, true, out var statusEnum) ? statusEnum : null,
                Priority = !string.IsNullOrEmpty(priority) && Enum.TryParse<WorkOrderPriority>(priority, true, out var priorityEnum) ? priorityEnum : null,
                AssignedTechnicianId = assignedTechnicianId,
                WorkOrderTypeId = workOrderTypeId,
                ScheduledDateFrom = scheduledDateFrom,
                ScheduledDateTo = scheduledDateTo,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var result = await _mediator.Send(query);
            return Ok(result);


        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<WorkOrderDto> GetWorkOrder(Guid id)
        {
            // This would need a GetWorkOrderByIdQuery implementation
            // For now, returning a placeholder
            return NotFound("GetWorkOrderByIdQuery not implemented yet");
        }

        [HttpGet("view")]
        [ProducesResponseType(typeof(WorkOrderViewListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WorkOrderViewListResponse>> GetWorkOrderViewList(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? status = null,
            [FromQuery] string? priority = null,
            [FromQuery] string? assignedTechnicianId = null,
            [FromQuery] bool? isOverdue = null,
            [FromQuery] bool? isToday = null,
            [FromQuery] string? sortBy = "CreatedOn",
            [FromQuery] bool sortDescending = true,
            [FromQuery] bool includeSummary = true)
        {
            if (pageNumber < 1)
                return BadRequest("Page number must be greater than 0");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100");

            var query = new GetWorkOrderViewListQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                Status = !string.IsNullOrEmpty(status) && Enum.TryParse<WorkOrderStatus>(status, true, out var statusEnum) ? statusEnum : null,
                Priority = !string.IsNullOrEmpty(priority) && Enum.TryParse<WorkOrderPriority>(priority, true, out var priorityEnum) ? priorityEnum : null,
                AssignedTechnicianId = assignedTechnicianId,
                IsOverdue = isOverdue,
                IsToday = isToday,
                SortBy = sortBy,
                SortDescending = sortDescending,
                IncludeSummary = includeSummary
            };


            var result = await _mediator.Send(query);
            return Ok(result);

        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(Dictionary<string, object>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Dictionary<string, object>>> GetWorkOrderSummary()
        {
            var query = new GetWorkOrderViewListQuery
            {
                PageNumber = 1,
                PageSize = 1,
                IncludeSummary = true
            };


            var result = await _mediator.Send(query);

            return Ok(new Dictionary<string, object>
            {
                ["StatusCounts"] = result.StatusCounts,
                ["PriorityCounts"] = result.PriorityCounts,
                ["OverdueCount"] = result.OverdueCount,
                ["TodayCount"] = result.TodayCount,
                ["TotalCount"] = result.TotalCount
            });


        }
    }
}
