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

        public async Task<IEnumerable<WorkOrder>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(wo => wo.Id == id && !wo.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatus status, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted && wo.Status == status)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByPriorityAsync(WorkOrderPriority priority, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted && wo.Priority == priority)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByAssignedTechnicianAsync(string technicianId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted && wo.Assignee == technicianId)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<WorkOrder> WorkOrders, int TotalCount)> GetPaginatedWithProjectionAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(wo => 
                    EF.Functions.Like(wo.WorkOrderNumber, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.Address, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.Description, $"%{searchTerm}%") ||
                    (wo.Assignee != null && EF.Functions.Like(wo.Assignee, $"%{searchTerm}%")));
            }

            if (status.HasValue)
            {
                query = query.Where(wo => wo.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(wo => wo.Priority == priority.Value);
            }

            if (!string.IsNullOrEmpty(assignedTechnicianId))
            {
                query = query.Where(wo => wo.Assignee == assignedTechnicianId);
            }

            if (scheduledDateFrom.HasValue)
            {
                query = query.Where(wo => wo.DueDate >= scheduledDateFrom.Value);
            }

            if (scheduledDateTo.HasValue)
            {
                query = query.Where(wo => wo.DueDate <= scheduledDateTo.Value);
            }

            // Apply sorting efficiently
            query = (sortBy?.ToLower() ?? "createdon") switch
            {
                "workordernumber" => sortDescending 
                    ? query.OrderByDescending(wo => wo.WorkOrderNumber)
                    : query.OrderBy(wo => wo.WorkOrderNumber),
                "priority" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Priority)
                    : query.OrderBy(wo => wo.Priority),
                "status" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Status)
                    : query.OrderBy(wo => wo.Status),
                "duedate" => sortDescending 
                    ? query.OrderByDescending(wo => wo.DueDate)
                    : query.OrderBy(wo => wo.DueDate),
                "assignee" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Assignee)
                    : query.OrderBy(wo => wo.Assignee),
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

        public async Task<IEnumerable<dynamic>> GetWorkOrderSummaryAsync(
            int pageNumber = 1,
            int pageSize = 10,
            WorkOrderStatus? status = null,
            WorkOrderPriority? priority = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(wo => !wo.IsDeleted);

            if (status.HasValue)
                query = query.Where(wo => wo.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(wo => wo.Priority == priority.Value);

            return await query
                .Select(wo => new
                {
                    wo.Id,
                    wo.WorkOrderNumber,
                    wo.Status,
                    wo.Priority,
                    wo.Assignee,
                    wo.DueDate,
                    wo.CreatedOn
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<dynamic> WorkOrders, int TotalCount)> GetWorkOrderDtosWithProjectionAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(wo => !wo.IsDeleted);

            // Apply filters efficiently
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(wo => 
                    EF.Functions.Like(wo.WorkOrderNumber, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.Address, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.Description, $"%{searchTerm}%") ||
                    (wo.Assignee != null && EF.Functions.Like(wo.Assignee, $"%{searchTerm}%")));
            }

            if (status.HasValue)
                query = query.Where(wo => wo.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(wo => wo.Priority == priority.Value);

            if (!string.IsNullOrEmpty(assignedTechnicianId))
                query = query.Where(wo => wo.Assignee == assignedTechnicianId);

            if (scheduledDateFrom.HasValue)
                query = query.Where(wo => wo.DueDate >= scheduledDateFrom.Value);

            if (scheduledDateTo.HasValue)
                query = query.Where(wo => wo.DueDate <= scheduledDateTo.Value);

            // Apply sorting efficiently
            query = (sortBy?.ToLower() ?? "createdon") switch
            {
                "workordernumber" => sortDescending 
                    ? query.OrderByDescending(wo => wo.WorkOrderNumber)
                    : query.OrderBy(wo => wo.WorkOrderNumber),
                "priority" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Priority)
                    : query.OrderBy(wo => wo.Priority),
                "status" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Status)
                    : query.OrderBy(wo => wo.Status),
                "duedate" => sortDescending 
                    ? query.OrderByDescending(wo => wo.DueDate)
                    : query.OrderBy(wo => wo.DueDate),
                "assignee" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Assignee)
                    : query.OrderBy(wo => wo.Assignee),
                _ => sortDescending 
                    ? query.OrderByDescending(wo => wo.CreatedOn)
                    : query.OrderBy(wo => wo.CreatedOn)
            };

            // Get total count 
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination and projection
            var workOrderDtos = await query
                .Select(wo => new
                {
                    wo.Id,
                    wo.WorkOrderNumber,
                    wo.Type,
                    wo.Priority,
                    wo.Status,
                    wo.Assignee,
                    wo.Address,
                    wo.Description,
                    wo.DueDate,
                    wo.CreatedOn,
                    wo.UpdatedOn
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (workOrderDtos, totalCount);
        }

        // View/List method for lightweight data - optimized for UI components
        public async Task<(IEnumerable<dynamic> WorkOrders, int TotalCount)> GetWorkOrderViewListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(wo => !wo.IsDeleted);

            // Apply filters efficiently
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(wo => 
                    EF.Functions.Like(wo.WorkOrderNumber, $"%{searchTerm}%") ||
                    EF.Functions.Like(wo.Address, $"%{searchTerm}%") ||
                    (wo.Assignee != null && EF.Functions.Like(wo.Assignee, $"%{searchTerm}%")));
            }

            if (status.HasValue)
                query = query.Where(wo => wo.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(wo => wo.Priority == priority.Value);

            if (!string.IsNullOrEmpty(assignedTechnicianId))
                query = query.Where(wo => wo.Assignee == assignedTechnicianId);

            // Special filters for view/list
            if (isOverdue.HasValue && isOverdue.Value)
            {
                var today = DateTime.Today;
                query = query.Where(wo => wo.DueDate.HasValue && wo.DueDate < today && 
                    wo.Status != WorkOrderStatus.Completed && wo.Status != WorkOrderStatus.Cancelled);
            }

            if (isToday.HasValue && isToday.Value)
            {
                var today = DateTime.Today;
                query = query.Where(wo => wo.DueDate.HasValue && wo.DueDate.Value.Date == today);
            }

            // Apply sorting efficiently
            query = (sortBy?.ToLower() ?? "createdon") switch
            {
                "workordernumber" => sortDescending 
                    ? query.OrderByDescending(wo => wo.WorkOrderNumber)
                    : query.OrderBy(wo => wo.WorkOrderNumber),
                "priority" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Priority)
                    : query.OrderBy(wo => wo.Priority),
                "status" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Status)
                    : query.OrderBy(wo => wo.Status),
                "duedate" => sortDescending 
                    ? query.OrderByDescending(wo => wo.DueDate)
                    : query.OrderBy(wo => wo.DueDate),
                "assignee" => sortDescending 
                    ? query.OrderByDescending(wo => wo.Assignee)
                    : query.OrderBy(wo => wo.Assignee),
                _ => sortDescending 
                    ? query.OrderByDescending(wo => wo.CreatedOn)
                    : query.OrderBy(wo => wo.CreatedOn)
            };

            // Get total count 
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination and projection
            var workOrderViews = await query
                .Select(wo => new
                {
                    wo.Id,
                    wo.WorkOrderNumber,
                    wo.Type,
                    wo.Priority,
                    wo.Status,
                    wo.Assignee,
                    wo.Address,
                    wo.DueDate,
                    wo.CreatedOn
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (workOrderViews, totalCount);
        }

        public async Task<Dictionary<string, object>> GetWorkOrderViewSummaryAsync(
            CancellationToken cancellationToken = default)
        {
            var today = DateTime.Today;
            var query = _dbSet.AsNoTracking().Where(wo => !wo.IsDeleted);

            var summary = new Dictionary<string, object>
            {
                ["TotalCount"] = await query.CountAsync(cancellationToken),
                ["PendingCount"] = await query.CountAsync(wo => wo.Status == WorkOrderStatus.Pending, cancellationToken),
                ["AssignedCount"] = await query.CountAsync(wo => wo.Status == WorkOrderStatus.Assigned, cancellationToken),
                ["InProgressCount"] = await query.CountAsync(wo => wo.Status == WorkOrderStatus.InProgress, cancellationToken),
                ["CompletedCount"] = await query.CountAsync(wo => wo.Status == WorkOrderStatus.Completed, cancellationToken),
                ["CancelledCount"] = await query.CountAsync(wo => wo.Status == WorkOrderStatus.Cancelled, cancellationToken),
                ["HighPriorityCount"] = await query.CountAsync(wo => wo.Priority == WorkOrderPriority.High, cancellationToken),
                ["MediumPriorityCount"] = await query.CountAsync(wo => wo.Priority == WorkOrderPriority.Medium, cancellationToken),
                ["LowPriorityCount"] = await query.CountAsync(wo => wo.Priority == WorkOrderPriority.Low, cancellationToken),
                ["OverdueCount"] = await query.CountAsync(wo => wo.DueDate.HasValue && wo.DueDate < today && wo.Status != WorkOrderStatus.Completed && wo.Status != WorkOrderStatus.Cancelled, cancellationToken),
                ["TodayCount"] = await query.CountAsync(wo => wo.DueDate.HasValue && wo.DueDate.Value.Date == today, cancellationToken)
            };

            return summary;
        }
    }
}