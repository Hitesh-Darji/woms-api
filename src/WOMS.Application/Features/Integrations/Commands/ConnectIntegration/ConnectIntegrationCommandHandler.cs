using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.Integrations.Services;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Integrations.Commands.ConnectIntegration
{
    public class ConnectIntegrationCommandHandler : IRequestHandler<ConnectIntegrationCommand>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIntegrationConnectionService _connectionService;

        public ConnectIntegrationCommandHandler(
            IIntegrationRepository integrationRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IIntegrationConnectionService connectionService)
        {
            _integrationRepository = integrationRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _connectionService = connectionService;
        }

        public async Task Handle(ConnectIntegrationCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var integration = await _integrationRepository.GetByIdAsync(request.Id, cancellationToken);
            if (integration == null)
            {
                throw new KeyNotFoundException($"Integration with ID {request.Id} not found.");
            }

            // Validate configuration based on integration type
            var isValid = await _connectionService.ValidateConfigurationAsync(integration.Name, request.Configuration);
            if (!isValid)
            {
                throw new ArgumentException($"Invalid configuration for {integration.Name}");
            }

            // Connect to the external service
            await _connectionService.ConnectAsync(integration.Name, request.Configuration);

            // Update status to Connected and set configuration
            integration.Status = WOMS.Domain.Enums.IntegrationStatus.Connected;
            integration.Configuration = request.Configuration;
            integration.ConnectedOn = DateTime.UtcNow;
            integration.UpdatedBy = userId;
            integration.UpdatedOn = DateTime.UtcNow;

            await _integrationRepository.UpdateAsync(integration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

