using MediatR;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Workflow.Commands.ConnectNodes
{
    public class ConnectNodesCommandHandler : IRequestHandler<ConnectNodesCommand, bool>
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConnectNodesCommandHandler(IWorkflowRepository workflowRepository, IUnitOfWork unitOfWork)
        {
            _workflowRepository = workflowRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ConnectNodesCommand request, CancellationToken cancellationToken)
        {
            var fromNode = await _workflowRepository.GetNodeByIdAsync(request.FromNodeId, cancellationToken);
            var toNode = await _workflowRepository.GetNodeByIdAsync(request.ToNodeId, cancellationToken);

            if (fromNode == null || toNode == null)
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

            // Add the new connection if it doesn't exist
            var connectionId = request.ToNodeId.ToString();
            if (!connections.Contains(connectionId))
            {
                connections.Add(connectionId);
                await _workflowRepository.UpdateNodeConnectionsAsync(request.FromNodeId, connections, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
    }
}
