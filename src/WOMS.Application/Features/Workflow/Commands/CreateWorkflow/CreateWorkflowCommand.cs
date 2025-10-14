using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Commands.CreateWorkflow
{
    public class CreateWorkflowCommand : IRequest<WorkflowDto>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = "General";
    }
}
