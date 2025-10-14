using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Commands.UpdateWorkflow
{
    public class UpdateWorkflowCommand : IRequest<WorkflowDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = "General";
        public bool IsActive { get; set; } = true;
    }
}
