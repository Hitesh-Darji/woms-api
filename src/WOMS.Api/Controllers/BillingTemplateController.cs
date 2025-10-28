using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.BillingTemplates.Commands.CreateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.Commands.DeleteBillingTemplate;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Application.Features.BillingTemplates.Queries.GetBillingTemplateById;
using WOMS.Application.Features.BillingTemplates.Queries.GetAllBillingTemplates;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public BillingTemplateController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(BillingTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BillingTemplateDto>> CreateBillingTemplate([FromBody] CreateBillingTemplateDto createBillingTemplateDto)
        {
            if (createBillingTemplateDto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = _mapper.Map<CreateBillingTemplateCommand>(createBillingTemplateDto);
            if (command == null)
            {
                return BadRequest("Failed to map request to command.");
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBillingTemplate), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BillingTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BillingTemplateDto>> GetBillingTemplate(Guid id)
        {
            var query = new GetBillingTemplateByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BillingTemplateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<BillingTemplateDto>>> GetAllBillingTemplates([FromQuery] string? customerId = null)
        {
            var query = new GetAllBillingTemplatesQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BillingTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BillingTemplateDto>> UpdateBillingTemplate(Guid id, [FromBody] UpdateBillingTemplateDto updateBillingTemplateDto)
        {
            if (updateBillingTemplateDto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = _mapper.Map<UpdateBillingTemplateCommand>((updateBillingTemplateDto, id));
            if (command == null)
            {
                return BadRequest("Failed to map request to command.");
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBillingTemplate(Guid id)
        {
            var command = new DeleteBillingTemplateCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
