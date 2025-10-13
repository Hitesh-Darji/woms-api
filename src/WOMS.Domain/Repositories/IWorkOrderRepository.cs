using WOMS.Domain.Entities;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Repositories
{
    public interface IWorkOrderRepository : IRepository<WorkOrder>
    {
        Task<IEnumerable<WorkOrder>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
        Task<WorkOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrder>> GetByPriorityAsync(WorkOrderPriority priority, CancellationToken cancellationToken = default);
        Task<IEnumerable<WorkOrder>> GetByAssignedTechnicianAsync(string technicianId, CancellationToken cancellationToken = default);
        
        Task<(IEnumerable<WorkOrder> WorkOrders, int TotalCount)> GetPaginatedWithProjectionAsync(
            int pageNumber, 
            int pageSize, 
            string? searchTerm = null,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            string? assignedTechnicianId = null,
            Guid? workOrderTypeId = null,
            DateTime? scheduledDateFrom = null,
            DateTime? scheduledDateTo = null,
            string? sortBy = "CreatedOn",
            bool sortDescending = true,
            CancellationToken cancellationToken = default);
            
        Task<IEnumerable<dynamic>> GetWorkOrderSummaryAsync(
            int pageNumber = 1,
            int pageSize = 10,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            CancellationToken cancellationToken = default);
            
        Task<(IEnumerable<dynamic> WorkOrders, int TotalCount)> GetWorkOrderDtosWithProjectionAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            string? assignedTechnicianId = null,
            Guid? workOrderTypeId = null,
            DateTime? scheduledDateFrom = null,
            DateTime? scheduledDateTo = null,
            string? sortBy = "CreatedOn",
            bool sortDescending = true,
            CancellationToken cancellationToken = default);
            
        Task<(IEnumerable<dynamic> WorkOrders, int TotalCount)> GetWorkOrderViewListAsync(
            int pageNumber = 1,
            int pageSize = 20,
            string? searchTerm = null,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            string? assignedTechnicianId = null,
            bool? isOverdue = null,
            bool? isToday = null,
            string? sortBy = "CreatedOn",
            bool sortDescending = true,
            CancellationToken cancellationToken = default);
            
        Task<Dictionary<string, object>> GetWorkOrderViewSummaryAsync(
            CancellationToken cancellationToken = default);
    }
}
