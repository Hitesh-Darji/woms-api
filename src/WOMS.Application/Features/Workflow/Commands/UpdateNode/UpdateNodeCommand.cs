using MediatR;
using WOMS.Application.Features.Workflow.DTOs;

namespace WOMS.Application.Features.Workflow.Commands.UpdateNode
{
    public class UpdateNodeCommand : IRequest<WorkflowNodeDto>
    {
        public Guid NodeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public int OrderIndex { get; set; }
    }
}
