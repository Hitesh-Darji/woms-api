using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.RouteOptimization.Commands.OptimizeAllRoutes;
using WOMS.Application.Features.RouteOptimization.Commands.SendRoute;
using WOMS.Application.Features.RouteOptimization.Commands.ReorderWorkOrders;
using WOMS.Application.Features.RouteOptimization.DTOs;
using WOMS.Application.Features.RouteOptimization.Queries.GetOptimizationMetrics;
using WOMS.Application.Features.RouteOptimization.Queries.GetTechnicianRoutes;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RouteOptimizationController : BaseController
    {
        private readonly IMediator _mediator;

        public RouteOptimizationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("Metrics")]
        [ProducesResponseType(typeof(RouteOptimizationMetricsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RouteOptimizationMetricsDto>> GetOptimizationMetrics(
            [FromQuery] DateTime? date = null)
        {
            var query = new GetOptimizationMetricsQuery
            {
                Date = date ?? DateTime.Today
            };

            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Route optimization metrics retrieved successfully", true, result, null);
        }

        [HttpGet("TechnicianRoutes")]
        [ProducesResponseType(typeof(TechnicianRoutesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TechnicianRoutesResponse>> GetTechnicianRoutes(
            [FromQuery] DateTime? date = null,
            [FromQuery] string? technicianId = null)
        {
            var query = new GetTechnicianRoutesQuery
            {
                Date = date ?? DateTime.Today,
                TechnicianId = technicianId
            };

            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Technician routes retrieved successfully", true, result, null);
        }



        [HttpPost("OptimizeAll")]
        [ProducesResponseType(typeof(OptimizeAllRoutesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OptimizeAllRoutesResponse>> OptimizeAllRoutes([FromBody] OptimizeAllRoutesRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new OptimizeAllRoutesCommand
            {
                Date = request.Date,
                ForceReoptimization = request.ForceReoptimization
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status200OK, "All routes optimized successfully", true, result, null);
        }

        [HttpPost("SendRoute/{routeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SendRoute(Guid routeId)
        {
            var command = new SendRouteCommand { RouteId = routeId };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Route with ID {routeId} not found.");

            return Ok(new { message = "Route sent to technician successfully" });
        }


        [HttpPost("ReorderWorkOrders")]
        [ProducesResponseType(typeof(ReorderWorkOrdersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReorderWorkOrdersResponse>> ReorderWorkOrders([FromBody] ReorderWorkOrdersRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new ReorderWorkOrdersCommand
            {
                RouteId = request.RouteId,
                WorkOrderId = request.WorkOrderId,
                Direction = request.Direction
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status200OK, result.Message, result.Success, result, null);
        }
    }
}
