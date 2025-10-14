using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Domain.Enums;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken = default)
        {
            return await FindAsync(wo => wo.Status == status && !wo.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByAssignedUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(wo => wo.Assignee == userId && !wo.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByPriorityAsync(WorkOrderPriority priority, CancellationToken cancellationToken = default)
        {
            return await FindAsync(wo => wo.Priority == priority && !wo.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetOverdueAsync(CancellationToken cancellationToken = default)
        {
            return await FindAsync(wo => wo.DueDate < DateTime.UtcNow && 
                                        wo.Status != WorkOrderStatus.Completed && 
                                        wo.Status != WorkOrderStatus.Cancelled && 
                                        !wo.IsDeleted, cancellationToken);
        }

        public async Task<(IEnumerable<WorkOrder> WorkOrders, int TotalCount)> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            string? assignedToUserId = null,
            string? sortBy = "CreatedAt",
            bool sortDescending = true,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryable()
                .AsNoTracking()
                .Where(wo => !wo.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(wo =>
                    EF.Functions.Like(wo.Customer, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.Description, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.WorkOrderNumber, $"%{searchTerm}%"));
            }

            if (status.HasValue)
            {
                query = query.Where(wo => wo.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(wo => wo.Priority == priority.Value);
            }

            if (!string.IsNullOrEmpty(assignedToUserId))
            {
                query = query.Where(wo => wo.Assignee == assignedToUserId);
            }

            // Apply sorting
            query = (sortBy?.ToLower() ?? "createdat") switch
            {
                "customer" => sortDescending
                    ? query.OrderByDescending(wo => wo.Customer)
                    : query.OrderBy(wo => wo.Customer),
                "status" => sortDescending
                    ? query.OrderByDescending(wo => wo.Status)
                    : query.OrderBy(wo => wo.Status),
                "priority" => sortDescending
                    ? query.OrderByDescending(wo => wo.Priority)
                    : query.OrderBy(wo => wo.Priority),
                "duedate" => sortDescending
                    ? query.OrderByDescending(wo => wo.DueDate)
                    : query.OrderBy(wo => wo.DueDate),
                _ => sortDescending
                    ? query.OrderByDescending(wo => wo.CreatedOn)
                    : query.OrderBy(wo => wo.CreatedOn)
            };

            // Get total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var workOrders = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (workOrders, totalCount);
        }

        public async Task<WorkOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(wo => wo.Id == id && !wo.IsDeleted, cancellationToken);
        }

        public async Task SoftDeleteAsync(Guid id, string deletedBy, CancellationToken cancellationToken = default)
        {
            var workOrder = await GetByIdAsync(id, cancellationToken);
            if (workOrder != null)
            {
                workOrder.IsDeleted = true;
                workOrder.DeletedBy = Guid.Parse(deletedBy);
                workOrder.DeletedOn = DateTime.UtcNow;
                await UpdateAsync(workOrder, cancellationToken);
            }
        }
    }
}