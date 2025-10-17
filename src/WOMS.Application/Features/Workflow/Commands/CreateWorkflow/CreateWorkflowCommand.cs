using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflow
{
    public class CreateWorkflowCommand : IRequest<WorkflowDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WorkflowCategory Category { get; set; } = WorkflowCategory.General;
        public List<WorkflowNodeDto>? Nodes { get; set; }
    }
}
