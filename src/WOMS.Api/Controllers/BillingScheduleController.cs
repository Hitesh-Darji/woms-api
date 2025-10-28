using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.BillingSchedules.Commands.CreateBillingSchedule;
using WOMS.Application.Features.BillingSchedules.Commands.DeleteBillingSchedule;
using WOMS.Application.Features.BillingSchedules.Commands.UpdateBillingSchedule;
using WOMS.Application.Features.BillingSchedules.DTOs;
using WOMS.Application.Features.BillingSchedules.Queries.GetAllBillingSchedules;
using WOMS.Application.Features.BillingSchedules.Queries.GetBillingScheduleById;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingScheduleController : BaseController
    {
        private readonly IMediator _mediator;

        public BillingScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BillingScheduleDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<BillingScheduleDto>> Create([FromBody] CreateBillingScheduleDto dto)
        {
            var result = await _mediator.Send(new CreateBillingScheduleCommand { Dto = dto });
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BillingScheduleDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BillingScheduleDto>>> GetAll([FromQuery] bool? isActive = null)
        {
            var result = await _mediator.Send(new GetAllBillingSchedulesQuery { IsActive = isActive });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BillingScheduleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BillingScheduleDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBillingScheduleByIdQuery { Id = id });
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BillingScheduleDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<BillingScheduleDto>> Update(Guid id, [FromBody] UpdateBillingScheduleDto dto)
        {
            var result = await _mediator.Send(new UpdateBillingScheduleCommand { Id = id, Dto = dto });
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteBillingScheduleCommand { Id = id });
            return NoContent();
        }
    }
}


