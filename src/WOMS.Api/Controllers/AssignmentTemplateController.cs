using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.AssignmentTemplate.Commands.CreateAssignmentTemplate;
using WOMS.Application.Features.AssignmentTemplate.Commands.CopyAssignmentTemplate;
using WOMS.Application.Features.AssignmentTemplate.Commands.DeleteAssignmentTemplate;
using WOMS.Application.Features.AssignmentTemplate.Commands.UpdateAssignmentTemplate;
using WOMS.Application.Features.AssignmentTemplate.DTOs;
using WOMS.Application.Features.AssignmentTemplate.Queries.GetAllAssignmentTemplates;
using WOMS.Application.Features.AssignmentTemplate.Queries.GetAssignmentTemplateById;
using WOMS.Domain.Enums;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssignmentTemplateController : BaseController
    {
        private readonly IMediator _mediator;

        public AssignmentTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AssignmentTemplateListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AssignmentTemplateListResponse>> GetAllAssignmentTemplates(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] AssignmentTemplateStatus? status = null,
            [FromQuery] string sortBy = "CreatedOn",
            [FromQuery] bool sortDescending = true)
        {
            if (pageNumber < 1)
                return BadRequest("Page number must be greater than 0");

            if (pageSize < 1 || pageSize > 100)
                return BadRequest("Page size must be between 1 and 100");

            var query = new GetAllAssignmentTemplatesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                Status = status,
                SortBy = sortBy,
                SortDescending = sortDescending
            };

            var result = await _mediator.Send(query);
            return HandleResponse(StatusCodes.Status200OK, "Assignment templates retrieved successfully", true, result, null);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AssignmentTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AssignmentTemplateDto>> GetAssignmentTemplate(Guid id)
        {
            var query = new GetAssignmentTemplateByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound($"Assignment template with ID {id} not found.");

            return HandleResponse(StatusCodes.Status200OK, "Assignment template retrieved successfully", true, result, null);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AssignmentTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AssignmentTemplateDto>> CreateAssignmentTemplate([FromBody] CreateAssignmentTemplateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new CreateAssignmentTemplateCommand
            {
                Name = request.Name,
                Description = request.Description,
                Status = request.Status,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DaysOfWeek = request.DaysOfWeek,
                AutoAssignmentRules = request.AutoAssignmentRules
            };

            var result = await _mediator.Send(command);
            return HandleResponse(StatusCodes.Status201Created, "Assignment template created successfully", true, result, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AssignmentTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AssignmentTemplateDto>> UpdateAssignmentTemplate(Guid id, [FromBody] UpdateAssignmentTemplateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = new UpdateAssignmentTemplateCommand
                {
                    Id = id,
                    Name = request.Name,
                    Description = request.Description,
                    Status = request.Status,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    DaysOfWeek = request.DaysOfWeek,
                    WorkTypes = request.WorkTypes,
                    Zones = request.Zones,
                    PreferredTechnicians = request.PreferredTechnicians,
                    SkillsRequired = request.SkillsRequired,
                    AutoAssignmentRules = request.AutoAssignmentRules
                };

                var result = await _mediator.Send(command);
                return HandleResponse(StatusCodes.Status200OK, "Assignment template updated successfully", true, result, null);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("{id}/copy")]
        [ProducesResponseType(typeof(AssignmentTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AssignmentTemplateDto>> CopyAssignmentTemplate(Guid id)
        {
            try
            {
                var command = new CopyAssignmentTemplateCommand
                {
                    Id = id,
                    NewName = null // Always generate name automatically
                };

                var result = await _mediator.Send(command);
                return HandleResponse(StatusCodes.Status201Created, "Assignment template copied successfully", true, result, null);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAssignmentTemplate(Guid id)
        {
            var command = new DeleteAssignmentTemplateCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound($"Assignment template with ID {id} not found.");

            return NoContent();
        }
    }
}
