using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IWorkOrderAssignmentRepository : IRepository<WorkOrderAssignment>
    {
        Task<IEnumerable<WorkOrderAssignment>> GetByWorkOrderIdAsync(Guid workOrderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrderAssignment>> GetByTechnicianIdAsync(string technicianId, CancellationToken cancellationToken = default);
        Task<WorkOrderAssignment?> GetActiveAssignmentAsync(Guid workOrderId, CancellationToken cancellationToken = default);
        Task<bool> HasActiveAssignmentAsync(Guid workOrderId, CancellationToken cancellationToken = default);
    }
}
