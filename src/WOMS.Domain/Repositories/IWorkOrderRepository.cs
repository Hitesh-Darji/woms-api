using WOMS.Domain.Entities;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Repositories
{
    public interface IWorkOrderRepository : IRepository<WorkOrder>
    {
        Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrder>> GetByAssignedUserAsync(string userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrder>> GetByPriorityAsync(WorkOrderPriority priority, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrder>> GetOverdueAsync(CancellationToken cancellationToken = default);
        Task<(IEnumerable<WorkOrder> WorkOrders, int TotalCount)> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            string? assignedToUserId = null,
            string? sortBy = "CreatedAt",
            bool sortDescending = true,
            CancellationToken cancellationToken = default);
        Task<WorkOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(Guid id, string deletedBy, CancellationToken cancellationToken = default);
    }
}