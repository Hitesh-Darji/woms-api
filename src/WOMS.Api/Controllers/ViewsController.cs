using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.View.Commands.CreateView;
using WOMS.Application.Features.View.DTOs;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ViewDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ViewDto>> CreateView([FromBody] CreateViewDto createViewDto)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new CreateViewCommand
            {
                Name = createViewDto.Name,
                SelectedColumns = createViewDto.SelectedColumns,
                CreatedBy = userIdClaim
            };

            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetView), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ViewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ViewDto> GetView(Guid id)
        {
            // This would need a GetViewByIdQuery implementation
            // For now, returning a placeholder
            return NotFound("GetViewByIdQuery not implemented yet");
        }
    }
}
