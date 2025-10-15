using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.WorkflowStatus.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.WorkflowStatus.Commands.DeleteWorkflowStatus
{
    public class DeleteWorkflowStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteWorkflowStatusCommandHandler : IRequestHandler<DeleteWorkflowStatusCommand, bool>
    {
        private readonly IWorkflowStatusRepository _workflowStatusRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteWorkflowStatusCommandHandler(
            IWorkflowStatusRepository workflowStatusRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _workflowStatusRepository = workflowStatusRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(DeleteWorkflowStatusCommand request, CancellationToken cancellationToken)
        {
            var workflowStatus = await _workflowStatusRepository.GetByIdAsync(request.Id, cancellationToken);
            if (workflowStatus == null)
            {
                return false;
            }

            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            // Soft delete
            workflowStatus.IsDeleted = true;
            workflowStatus.DeletedBy = userId;
            workflowStatus.DeletedOn = DateTime.UtcNow;

            await _workflowStatusRepository.UpdateAsync(workflowStatus, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
