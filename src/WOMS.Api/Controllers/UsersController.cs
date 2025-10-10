using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Users.Commands.CreateUser;
using WOMS.Application.Features.Users.Commands.UpdateUser;
using WOMS.Application.Features.Users.Commands.DeleteUser;
using WOMS.Application.Features.Users.DTOs;
using WOMS.Application.Features.Users.Queries.GetUserById;
using WOMS.Application.Features.Users.Queries.GetAllUsers;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var createdBy))
            {
                return Unauthorized("User ID not found in token");
            }
            var command = new CreateUserCommand
            {
                FullName = createUserDto.FullName,
                Address = createUserDto.Address,
                City = createUserDto.City,
                PostalCode = createUserDto.PostalCode,
                Phone = createUserDto.Phone,
                Email = createUserDto.Email,
                RoleId = createUserDto.RoleId,
                CreatedBy = createdBy
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetUser), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var updatedBy))
            {
                return Unauthorized("User ID not found in token");
            }

            var command = new UpdateUserCommand
            {
                Id = id,
                FullName = updateUserDto.FullName,
                Address = updateUserDto.Address,
                City = updateUserDto.City,
                PostalCode = updateUserDto.PostalCode,
                Phone = updateUserDto.Phone,
                Email = updateUserDto.Email,
                RoleId = updateUserDto.RoleId,
                UpdatedBy = updatedBy
            };

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand
            {
                Id = id
            };

            try
            {
                var result = await _mediator.Send(command);
                
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
