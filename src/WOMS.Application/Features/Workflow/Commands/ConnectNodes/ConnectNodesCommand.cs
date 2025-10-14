using MediatR;

namespace WOMS.Application.Features.Workflow.Commands.ConnectNodes
{
    public class ConnectNodesCommand : IRequest<bool>
    {
        public Guid FromNodeId { get; set; }
        public Guid ToNodeId { get; set; }
    }
}
