using MediatR;

namespace WOMS.Application.Features.Workflow.Commands.ToggleWorkflowStatus
{
    public class ToggleWorkflowStatusCommand : IRequest<ToggleWorkflowStatusResponse>
    {
        public Guid WorkflowId { get; set; }
        public bool IsActive { get; set; }
    }
}
