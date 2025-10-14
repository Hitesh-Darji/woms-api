using MediatR;

namespace WOMS.Application.Features.Workflow.Commands.DeleteNode
{
    public class DeleteNodeCommand : IRequest<bool>
    {
        public Guid NodeId { get; set; }
    }
}
