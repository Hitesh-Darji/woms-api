using MediatR;
using WOMS.Application.Features.Workflow.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.Commands.AddNode
{
    public class AddNodeCommand : IRequest<WorkflowNodeDto>
    {
        public Guid WorkflowId { get; set; }
        public WorkflowNodeType Type { get; set; } = WorkflowNodeType.Start;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public int OrderIndex { get; set; }
    }
}
