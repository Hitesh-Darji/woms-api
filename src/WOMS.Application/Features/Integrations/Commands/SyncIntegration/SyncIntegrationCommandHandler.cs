using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.Integrations.Services;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Integrations.Commands.SyncIntegration
{
    public class SyncIntegrationCommandHandler : IRequestHandler<SyncIntegrationCommand, SyncIntegrationResult>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIntegrationSyncService _syncService;

        public SyncIntegrationCommandHandler(
            IIntegrationRepository integrationRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IIntegrationSyncService syncService)
        {
            _integrationRepository = integrationRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _syncService = syncService;
        }

        public async Task<SyncIntegrationResult> Handle(SyncIntegrationCommand request, CancellationToken cancellationToken)
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

            if (integration.Status != Domain.Enums.IntegrationStatus.Connected)
            {
                return new SyncIntegrationResult
                {
                    Success = false,
                    Message = $"Integration is not connected.",
                    SyncStatus = integration.SyncStatus
                };
            }

            if (string.IsNullOrEmpty(integration.Configuration))
            {
                return new SyncIntegrationResult
                {
                    Success = false,
                    Message = $"Integration has no configuration.",
                    SyncStatus = Domain.Enums.SyncStatus.Failed
                };
            }

            try
            {
                // Perform the actual sync based on integration type
                var syncResult = await _syncService.SyncAsync(integration.Name, integration.Configuration);

                // Update integration with sync results
                integration.LastSyncOn = DateTime.UtcNow;
                integration.SyncStatus = syncResult.Success 
                    ? Domain.Enums.SyncStatus.Synced 
                    : Domain.Enums.SyncStatus.Failed;
                integration.UpdatedBy = userId;
                integration.UpdatedOn = DateTime.UtcNow;

                await _integrationRepository.UpdateAsync(integration, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SyncIntegrationResult
                {
                    Success = syncResult.Success,
                    Message = syncResult.Success 
                        ? $"Successfully synced {integration.Name}" 
                        : $"Sync failed for {integration.Name}: {syncResult.Message}",
                    LastSyncOn = integration.LastSyncOn,
                    SyncStatus = integration.SyncStatus
                };
            }
            catch (Exception ex)
            {
                // Update integration with failure
                integration.SyncStatus = Domain.Enums.SyncStatus.Failed;
                integration.UpdatedBy = userId;
                integration.UpdatedOn = DateTime.UtcNow;

                await _integrationRepository.UpdateAsync(integration, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SyncIntegrationResult
                {
                    Success = false,
                    Message = $"Sync failed for {integration.Name}: {ex.Message}",
                    SyncStatus = Domain.Enums.SyncStatus.Failed
                };
            }
        }
    }
}


