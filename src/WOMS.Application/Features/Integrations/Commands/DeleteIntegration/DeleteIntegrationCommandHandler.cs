using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Integrations.Commands.DeleteIntegration
{
    public class DeleteIntegrationCommandHandler : IRequestHandler<DeleteIntegrationCommand, Unit>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteIntegrationCommandHandler(
            IIntegrationRepository integrationRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _integrationRepository = integrationRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(DeleteIntegrationCommand request, CancellationToken cancellationToken)
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

            // Soft delete
            integration.IsDeleted = true;
            integration.DeletedBy = userId;
            integration.DeletedOn = DateTime.UtcNow;

            await _integrationRepository.UpdateAsync(integration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

