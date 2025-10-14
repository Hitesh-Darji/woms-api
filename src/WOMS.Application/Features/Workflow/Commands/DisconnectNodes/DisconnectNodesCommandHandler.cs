using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Workflow.Commands.DisconnectNodes
{
    public class DisconnectNodesCommandHandler : IRequestHandler<DisconnectNodesCommand, bool>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DisconnectNodesCommandHandler(IWorkflowRepository workflowRepository, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DisconnectNodesCommand request, CancellationToken cancellationToken)
        {
            var fromNode = await _workflowRepository.GetNodeByIdAsync(request.FromNodeId, cancellationToken);
            if (fromNode == null)
            {
                return false;
            }

            // Parse existing connections
            var connections = new List<string>();
            if (!string.IsNullOrEmpty(fromNode.Connections))
            {
                try
                {
                    connections = JsonSerializer.Deserialize<List<string>>(fromNode.Connections) ?? new List<string>();
                }
                catch
                {
                    connections = new List<string>();
                }
            }

            // Remove the connection
            var connectionId = request.ToNodeId.ToString();
            connections.Remove(connectionId);

            await _workflowRepository.UpdateNodeConnectionsAsync(request.FromNodeId, connections, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
