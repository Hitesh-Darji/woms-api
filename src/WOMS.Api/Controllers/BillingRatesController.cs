using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.BillingRates.Commands.CreateBillingRate;
using WOMS.Application.Features.BillingRates.Commands.UpdateBillingRate;
using WOMS.Application.Features.BillingRates.Commands.DeleteBillingRate;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Application.Features.BillingRates.Queries.GetBillingRateById;
using WOMS.Application.Features.BillingRates.Queries.GetAllBillingRates;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingRatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public BillingRatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BillingRateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BillingRateDto>> CreateBillingRate([FromBody] CreateBillingRateDto createBillingRateDto)
        {
            if (createBillingRateDto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = new CreateBillingRateCommand
            {
                Dto = createBillingRateDto
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBillingRate), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BillingRateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BillingRateDto>> GetBillingRate(Guid id)
        {
            var query = new GetBillingRateByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BillingRateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<BillingRateDto>>> GetAllBillingRates(
            [FromQuery] bool? isActive = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var query = new GetAllBillingRatesQuery 
            { 
                IsActive = isActive,
                StartDate = startDate,
                EndDate = endDate
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BillingRateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BillingRateDto>> UpdateBillingRate(Guid id, [FromBody] UpdateBillingRateDto updateBillingRateDto)
        {
            if (updateBillingRateDto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = new UpdateBillingRateCommand
            {
                Id = id,
                Dto = updateBillingRateDto
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBillingRate(Guid id)
        {
            var command = new DeleteBillingRateCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
