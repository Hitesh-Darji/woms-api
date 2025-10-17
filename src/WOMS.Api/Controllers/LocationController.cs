using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Location.Commands.CreateLocation;
using WOMS.Application.Features.Location.Commands.UpdateLocation;
using WOMS.Application.Features.Location.Commands.DeleteLocation;
using WOMS.Application.Features.Location.DTOs;
using WOMS.Application.Features.Location.Queries.GetAllLocations;
using WOMS.Application.Features.Location.Queries.GetLocationById;
using WOMS.Domain.Entities;
using AutoMapper;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LocationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LocationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LocationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<LocationDto>>> GetAllLocations(
            [FromQuery] bool includeInactive = false)
        {
            var query = new GetAllLocationsQuery { IncludeInactive = includeInactive };
            var locations = await _mediator.Send(query);
            return Ok(locations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LocationDto>> GetLocationById(Guid id)
        {
            var query = new GetLocationByIdQuery { Id = id };
            var location = await _mediator.Send(query);
            
            if (location == null)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] CreateLocationDto createLocationDto)
        {
            var command = _mapper.Map<CreateLocationCommand>(createLocationDto);
            command.CreatedBy = UserId;

            var location = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LocationDto>> UpdateLocation(Guid id, [FromBody] UpdateLocationDto updateLocationDto)
        {
            var command = _mapper.Map<UpdateLocationCommand>(updateLocationDto);
            command.Id = id;
            command.UpdatedBy = UserId;

            var location = await _mediator.Send(command);
            return Ok(location);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteLocation(Guid id)
        {
            var command = new DeleteLocationCommand { Id = id };
            var result = await _mediator.Send(command);
            
            if (!result)
            {
                return NotFound($"Location with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
