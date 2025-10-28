using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOMS.Application.Features.Integrations.Commands.ConnectIntegration;
using WOMS.Application.Features.Integrations.Commands.CreateIntegration;
using WOMS.Application.Features.Integrations.Commands.DeleteIntegration;
using WOMS.Application.Features.Integrations.Commands.SyncIntegration;
using WOMS.Application.Features.Integrations.Commands.UpdateIntegration;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Application.Features.Integrations.Queries.GetAllIntegrations;
using WOMS.Application.Features.Integrations.Queries.GetIntegrationById;
using WOMS.Domain.Enums;

namespace WOMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IntegrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all integrations with optional filters
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<IntegrationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<IntegrationDto>>> GetAllIntegrations(
            [FromQuery] IntegrationCategory? category = null,
            [FromQuery] IntegrationStatus? status = null,
            [FromQuery] bool? isActive = null)
        {
            var query = new GetAllIntegrationsQuery
            {
                Category = category,
                Status = status,
                IsActive = isActive
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get integration by ID
        /// </summary>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IntegrationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IntegrationDto>> GetIntegration(Guid id)
        {
            var query = new GetIntegrationByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Create a new integration
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(IntegrationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IntegrationDto>> CreateIntegration([FromBody] CreateIntegrationDto createIntegrationDto)
        {
            if (createIntegrationDto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = new CreateIntegrationCommand
            {
                Dto = createIntegrationDto
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetIntegration), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update an existing integration
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IntegrationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IntegrationDto>> UpdateIntegration(Guid id, [FromBody] UpdateIntegrationDto updateIntegrationDto)
        {
            if (updateIntegrationDto == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            var command = new UpdateIntegrationCommand
            {
                Id = id,
                Dto = updateIntegrationDto
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Delete an integration (soft delete)
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteIntegration(Guid id)
        {
            var command = new DeleteIntegrationCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Connect an integration with configuration
        /// </summary>
        [Authorize]
        [HttpPost("{id}/connect")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ConnectIntegration(Guid id, [FromBody] ConnectIntegrationDto connectIntegrationDto)
        {
            if (connectIntegrationDto == null || string.IsNullOrEmpty(connectIntegrationDto.Configuration))
            {
                return BadRequest("Configuration is required.");
            }

            var command = new ConnectIntegrationCommand
            {
                Id = id,
                Configuration = connectIntegrationDto.Configuration
            };

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Disconnect an integration
        /// </summary>
        [Authorize]
        [HttpPost("{id}/disconnect")]
        [ProducesResponseType(typeof(IntegrationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IntegrationDto>> DisconnectIntegration(Guid id)
        {
            var command = new UpdateIntegrationCommand
            {
                Id = id,
                Dto = new UpdateIntegrationDto
                {
                    Status = IntegrationStatus.Available,
                    Configuration = null
                }
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get integration sync status
        /// </summary>
        [Authorize]
        [HttpGet("{id}/sync-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<object>> GetSyncStatus(Guid id)
        {
            var query = new GetIntegrationByIdQuery { Id = id };
            var integration = await _mediator.Send(query);

            if (integration == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                status = integration.Status,
                connectedOn = integration.ConnectedOn,
                lastSyncOn = integration.LastSyncOn,
                syncStatus = integration.SyncStatus,
                isActive = integration.IsActive
            });
        }

        /// <summary>
        /// Trigger manual sync for connected integration
        /// </summary>
        [Authorize]
        [HttpPost("{id}/sync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<object>> SyncIntegration(Guid id)
        {
            try
            {
                var command = new SyncIntegrationCommand { Id = id };
                var result = await _mediator.Send(command);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        lastSyncOn = result.LastSyncOn,
                        syncStatus = result.SyncStatus
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = result.Message,
                        lastSyncOn = result.LastSyncOn,
                        syncStatus = result.SyncStatus
                    });
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Sync failed: {ex.Message}"
                });
            }
        }
    }
}

