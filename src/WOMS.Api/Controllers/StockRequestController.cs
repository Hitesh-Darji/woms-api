using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.StockRequest.Commands.CreateStockRequest;
using WOMS.Application.Features.StockRequest.Commands.UpdateStockRequest;
using WOMS.Application.Features.StockRequest.Commands.DeleteStockRequest;
using WOMS.Application.Features.StockRequest.Commands.ApproveStockRequest;
using WOMS.Application.Features.StockRequest.DTOs;
using WOMS.Application.Features.StockRequest.Queries.GetAllStockRequests;
using WOMS.Application.Features.StockRequest.Queries.GetStockRequestById;
using WOMS.Domain.Enums;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockRequestController : BaseController
    {
        private readonly IMediator _mediator;

        public StockRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StockRequestDto), StatusCodes.Status201Created)]
        [Authorize]
        public async Task<ActionResult<StockRequestDto>> CreateStockRequest([FromBody] CreateStockRequestDto createStockRequestDto)
        {
            // Check if User is authenticated
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("User is not authenticated");
            }

            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token");
            }

            // Validate the request
            if (createStockRequestDto == null)
            {
                return BadRequest("Request body cannot be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateStockRequestCommand
            {
                FromLocationId = createStockRequestDto.FromLocationId,
                ToLocationId = createStockRequestDto.ToLocationId,
                Notes = createStockRequestDto.Notes,
                WorkOrderId = createStockRequestDto.WorkOrderId,
                RequestItems = createStockRequestDto.RequestItems,
                CreatedBy = userIdClaim
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetStockRequest), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(StockRequestListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StockRequestListResponse>> GetAllStockRequests(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] StockRequestStatus? status = null,
            [FromQuery] Guid? fromLocationId = null,
            [FromQuery] Guid? toLocationId = null,
            [FromQuery] string? requesterId = null,
            [FromQuery] string sortBy = "RequestDate",
            [FromQuery] bool sortDescending = true)
        {
            var query = new GetAllStockRequestsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                Status = status,
                FromLocationId = fromLocationId,
                ToLocationId = toLocationId,
                RequesterId = requesterId,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StockRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StockRequestDto>> GetStockRequest(Guid id)
        {
            var query = new GetStockRequestByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(StockRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StockRequestDto>> UpdateStockRequest(Guid id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            // Check if User is authenticated
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("User is not authenticated");
            }

            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new UpdateStockRequestCommand
            {
                Id = id,
                Notes = updateStockRequestDto.Notes,
                RequestItems = updateStockRequestDto.RequestItems,
                UpdatedBy = userIdClaim
            };

            try
            {
                var result = await _mediator.Send(command);
                if (result == null)
                {
                    return NotFound($"Stock Request with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return Conflict("The stock request you are trying to update has been modified or deleted by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating stock request: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("{id}/approve")]
        [ProducesResponseType(typeof(StockRequestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StockRequestDto>> ApproveStockRequest(Guid id, [FromBody] ApproveStockRequestDto approveStockRequestDto)
        {
            // Check if User is authenticated
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("User is not authenticated");
            }

            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new ApproveStockRequestCommand
            {
                Id = id,
                ApprovalNotes = approveStockRequestDto.ApprovalNotes,
                RequestItems = approveStockRequestDto.RequestItems,
                ApprovedBy = userIdClaim
            };

            try
            {
                var result = await _mediator.Send(command);
                if (result == null)
                {
                    return NotFound($"Stock Request with ID {id} not found.");
                }
                return Ok(result);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return Conflict("The stock request you are trying to approve has been modified or deleted by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error approving stock request: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteStockRequest(Guid id)
        {
            // Check if User is authenticated
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized("User is not authenticated");
            }

            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new DeleteStockRequestCommand
            {
                Id = id,
                DeletedBy = userIdClaim
            };

            try
            {
                var result = await _mediator.Send(command);
                if (!result)
                {
                    return NotFound($"Stock Request with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                return Conflict("The stock request you are trying to delete has been modified or deleted by another user. Please refresh and try again.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting stock request: {ex.Message}");
            }
        }

    }
}
