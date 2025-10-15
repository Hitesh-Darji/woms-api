using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Workflow.Commands.ToggleWorkflowStatus
{
    public class ToggleWorkflowStatusHandler : IRequestHandler<ToggleWorkflowStatusCommand, ToggleWorkflowStatusResponse>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ToggleWorkflowStatusHandler(IWorkflowRepository workflowRepository, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ToggleWorkflowStatusResponse> Handle(ToggleWorkflowStatusCommand request, CancellationToken cancellationToken)
        {
            var workflow = await _workflowRepository.GetByIdAsync(request.WorkflowId, cancellationToken);

            if (workflow == null)
            {
                throw new ArgumentException($"Workflow with ID {request.WorkflowId} not found.");
            }

            // Update the workflow status
            workflow.IsActive = request.IsActive;
            workflow.UpdatedAt = DateTime.UtcNow;

            await _workflowRepository.UpdateAsync(workflow, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var action = request.IsActive ? "activated" : "deactivated";
            var message = $"Workflow '{workflow.Name}' has been {action} successfully.";

            return new ToggleWorkflowStatusResponse
            {
                WorkflowId = workflow.Id,
                Name = workflow.Name,
                IsActive = workflow.IsActive,
                Message = message
            };
        }
    }
}
