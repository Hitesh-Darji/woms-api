using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Commands.UpdateWorkflow
{
    public class UpdateWorkflowCommand : IRequest<WorkflowDto>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WorkflowCategory Category { get; set; } = WorkflowCategory.General;
        public bool IsActive { get; set; } = true;
        public List<WorkflowNodeDto>? Nodes { get; set; }
    }
}
