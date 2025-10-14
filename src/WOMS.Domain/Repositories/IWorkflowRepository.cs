using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IWorkflowRepository : IRepository<Workflow>
    {
        Task<IEnumerable<Workflow>> GetAllWithNodesAsync(CancellationToken cancellationToken = default);
        Task<Workflow?> GetByIdWithNodesAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Workflow>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
        Task<IEnumerable<Workflow>> GetActiveWorkflowsAsync(CancellationToken cancellationToken = default);
        Task<(IEnumerable<Workflow> Workflows, int TotalCount)> GetPaginatedAsync(
            int pageNumber, 
            int pageSize, 
            string? searchTerm = null,
            string? category = null,
            bool? isActive = null,
            string? sortBy = "CreatedAt",
            bool sortDescending = true,
            CancellationToken cancellationToken = default);
        Task<WorkflowNode?> GetNodeByIdAsync(Guid nodeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkflowNode>> GetNodesByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default);
        Task<bool> UpdateNodeConnectionsAsync(Guid nodeId, List<string> connections, CancellationToken cancellationToken = default);
        Task AddNodeAsync(WorkflowNode node, CancellationToken cancellationToken = default);
        void UpdateNode(WorkflowNode node, CancellationToken cancellationToken = default);
        Task DeleteNodeAsync(WorkflowNode node, CancellationToken cancellationToken = default);
    }
}
