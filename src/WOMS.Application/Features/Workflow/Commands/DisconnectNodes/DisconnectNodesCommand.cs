using MediatR;

namespace WOMS.Application.Features.Workflow.Commands.DisconnectNodes
{
    public class DisconnectNodesCommand : IRequest<bool>
    {
        public Guid FromNodeId { get; set; }
        public Guid ToNodeId { get; set; }
    }
}
