using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Forms.Commands.CreateFormTemplate;
using WOMS.Application.Features.Forms.Commands.DeleteFormTemplate;
using WOMS.Application.Features.Forms.Commands.UpdateFormTemplate;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Application.Features.Forms.Queries.GetAllFormTemplates;
using WOMS.Application.Features.Forms.Queries.GetFormTemplateById;
using WOMS.Application.Features.Forms.Queries.GetFormTemplatesByCategory;
using WOMS.Application.Features.Forms.Queries.GetActiveFormTemplates;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : BaseController
    {
        private readonly IMediator _mediator;

        public FormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<FormTemplateDto>> CreateFormTemplate([FromBody] CreateFormTemplateDto createFormTemplateDto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            var command = new CreateFormTemplateCommand
            {
                CreateFormTemplateDto = createFormTemplateDto,
                UserIdClaim = userIdClaim
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFormTemplate), new { id = result.Id }, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FormTemplateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FormTemplateDto>>> GetAllFormTemplates()
        {
            var query = new GetAllFormTemplatesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<FormTemplateDto>> GetFormTemplate(Guid id)
        {
            var query = new GetFormTemplateByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<FormTemplateDto>> UpdateFormTemplate(Guid id, [FromBody] UpdateFormTemplateDto updateFormTemplateDto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            var command = new UpdateFormTemplateCommand
            {
                Id = id,
                UpdateFormTemplateDto = updateFormTemplateDto,
                UserIdClaim = userIdClaim
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult> DeleteFormTemplate(Guid id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            var command = new DeleteFormTemplateCommand
            {
                Id = id,
                UserIdClaim = userIdClaim
            };

            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<FormTemplateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FormTemplateDto>>> GetFormTemplatesByCategory(string category)
        {
            var query = new GetFormTemplatesByCategoryQuery { Category = category };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(IEnumerable<FormTemplateDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FormTemplateDto>>> GetActiveFormTemplates()
        {
            var query = new GetActiveFormTemplatesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
