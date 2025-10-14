using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IWorkflowStatusRepository : IRepository<WorkflowStatus>
    {
        Task<IEnumerable<WorkflowStatus>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkflowStatus>> GetByOrderAsync(CancellationToken cancellationToken = default);
        Task<WorkflowStatus?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkflowStatusTransition>> GetTransitionsAsync(Guid workflowId, Guid fromStatusId, CancellationToken cancellationToken = default);
        Task<bool> CanTransitionAsync(Guid workflowId, Guid fromStatusId, Guid toStatusId, CancellationToken cancellationToken = default);
        Task<WorkflowStatus?> GetByIdWithTransitionsAsync(Guid workflowId, Guid statusId, CancellationToken cancellationToken = default);
    }
}
