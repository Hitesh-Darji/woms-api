using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Commands.AddNode
{
    public class AddNodeCommand : IRequest<WorkflowNodeDto>
    {
        public Guid WorkflowId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public int OrderIndex { get; set; }
    }
}
