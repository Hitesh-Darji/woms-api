using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
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

        public async Task<IEnumerable<WorkOrder>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted && wo.Status == status)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByPriorityAsync(string priority, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted && wo.Priority == priority)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkOrder>> GetByAssignedTechnicianAsync(Guid technicianId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking() 
                .Where(wo => !wo.IsDeleted && wo.AssignedTechnicianId == technicianId)
                .OrderByDescending(wo => wo.CreatedOn)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<WorkOrder> WorkOrders, int TotalCount)> GetPaginatedWithProjectionAsync(
            int pageNumber, 
            int pageSize, 
            string? searchTerm = null,
            string? status = null,
            string? priority = null,
            Guid? assignedTechnicianId = null,
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
                    EF.Functions.Like(wo.ServiceAddress, $"%{searchTerm}%") ||
                    (wo.MeterNumber != null && EF.Functions.Like(wo.MeterNumber, $"%{searchTerm}%")) ||
                    (wo.AssignedTechnicianName != null && EF.Functions.Like(wo.AssignedTechnicianName, $"%{searchTerm}%")));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(wo => wo.Status == status);
            }

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(wo => wo.Priority == priority);
            }

            if (assignedTechnicianId.HasValue)
            {
                query = query.Where(wo => wo.AssignedTechnicianId == assignedTechnicianId.Value);
            }

            if (workOrderTypeId.HasValue)
            {
                query = query.Where(wo => wo.WorkOrderTypeId == workOrderTypeId.Value);
            }

            if (scheduledDateFrom.HasValue)
            {
                query = query.Where(wo => wo.ScheduledDate >= scheduledDateFrom.Value);
            }

            if (scheduledDateTo.HasValue)
            {
                query = query.Where(wo => wo.ScheduledDate <= scheduledDateTo.Value);
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
                "scheduleddate" => sortDescending 
                    ? query.OrderByDescending(wo => wo.ScheduledDate)
                    : query.OrderBy(wo => wo.ScheduledDate),
                "assignedtechnicianname" => sortDescending 
                    ? query.OrderByDescending(wo => wo.AssignedTechnicianName)
                    : query.OrderBy(wo => wo.AssignedTechnicianName),
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
            string? status = null,
            string? priority = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(wo => !wo.IsDeleted);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(wo => wo.Status == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(wo => wo.Priority == priority);

            return await query
                .Select(wo => new
                {
                    wo.Id,
                    wo.WorkOrderNumber,
                    wo.Status,
                    wo.Priority,
                    wo.ServiceAddress,
                    wo.ScheduledDate,
                    wo.CreatedOn,
                    WorkOrderTypeName = wo.WorkOrderType != null ? wo.WorkOrderType.Name : null,
                    AssignedTechnicianName = wo.AssignedTechnicianName ?? 
                        (wo.AssignedTechnician != null ? wo.AssignedTechnician.FirstName + " " + wo.AssignedTechnician.LastName : null)
                })
                .OrderByDescending(wo => wo.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<dynamic> WorkOrders, int TotalCount)> GetWorkOrderDtosWithProjectionAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            string? status = null,
            string? priority = null,
            Guid? assignedTechnicianId = null,
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
                    EF.Functions.Like(wo.ServiceAddress, $"%{searchTerm}%") ||
                    (wo.MeterNumber != null && EF.Functions.Like(wo.MeterNumber, $"%{searchTerm}%")) ||
                    (wo.AssignedTechnicianName != null && EF.Functions.Like(wo.AssignedTechnicianName, $"%{searchTerm}%")));
            }

            if (!string.IsNullOrEmpty(status))
                query = query.Where(wo => wo.Status == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(wo => wo.Priority == priority);

            if (assignedTechnicianId.HasValue)
                query = query.Where(wo => wo.AssignedTechnicianId == assignedTechnicianId.Value);

            if (workOrderTypeId.HasValue)
                query = query.Where(wo => wo.WorkOrderTypeId == workOrderTypeId.Value);

            if (scheduledDateFrom.HasValue)
                query = query.Where(wo => wo.ScheduledDate >= scheduledDateFrom.Value);

            if (scheduledDateTo.HasValue)
                query = query.Where(wo => wo.ScheduledDate <= scheduledDateTo.Value);

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
                "scheduleddate" => sortDescending 
                    ? query.OrderByDescending(wo => wo.ScheduledDate)
                    : query.OrderBy(wo => wo.ScheduledDate),
                "assignedtechnicianname" => sortDescending 
                    ? query.OrderByDescending(wo => wo.AssignedTechnicianName)
                    : query.OrderBy(wo => wo.AssignedTechnicianName),
                _ => sortDescending 
                    ? query.OrderByDescending(wo => wo.CreatedOn)
                    : query.OrderBy(wo => wo.CreatedOn)
            };

            // Get total count efficiently
            var totalCount = await query.CountAsync(cancellationToken);

            // Project directly to anonymous objects - single query approach
            var workOrderDtos = await query
                .Select(wo => new
                {
                    wo.Id,
                    wo.WorkOrderNumber,
                    WorkflowId = wo.WorkflowId,
                    WorkOrderTypeId = wo.WorkOrderTypeId,
                    WorkOrderTypeName = wo.WorkOrderType != null ? wo.WorkOrderType.Name : null,
                    Priority = wo.Priority,
                    Status = wo.Status,
                    ServiceAddress = wo.ServiceAddress,
                    MeterNumber = wo.MeterNumber,
                    CurrentReading = wo.CurrentReading,
                    wo.AssignedTechnicianId,
                    AssignedTechnicianName = wo.AssignedTechnicianName ?? 
                        (wo.AssignedTechnician != null ? wo.AssignedTechnician.FirstName + " " + wo.AssignedTechnician.LastName : null),
                    wo.Notes,
                    ScheduledDate = wo.ScheduledDate,
                    StartedAt = wo.StartedAt,
                    CompletedAt = wo.CompletedAt,
                    CreatedAt = wo.CreatedOn,
                    UpdatedAt = wo.UpdatedOn,
                    DueDate = wo.ScheduledDate,
                    Utility = (string?)null, 
                    Make = (string?)null,
                    Model = (string?)null,
                    Size = (string?)null,
                    Location = (string?)null
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
            string? status = null,
            string? priority = null,
            Guid? assignedTechnicianId = null,
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
                    EF.Functions.Like(wo.ServiceAddress, $"%{searchTerm}%") ||
                    (wo.AssignedTechnicianName != null && EF.Functions.Like(wo.AssignedTechnicianName, $"%{searchTerm}%")));
            }

            if (!string.IsNullOrEmpty(status))
                query = query.Where(wo => wo.Status == status);

            if (!string.IsNullOrEmpty(priority))
                query = query.Where(wo => wo.Priority == priority);

            if (assignedTechnicianId.HasValue)
                query = query.Where(wo => wo.AssignedTechnicianId == assignedTechnicianId.Value);

            // Special filters for view/list
            if (isOverdue.HasValue && isOverdue.Value)
            {
                var today = DateTime.Today;
                query = query.Where(wo => wo.ScheduledDate.HasValue && wo.ScheduledDate < today && 
                    wo.Status != "completed" && wo.Status != "cancelled");
            }

            if (isToday.HasValue && isToday.Value)
            {
                var today = DateTime.Today;
                query = query.Where(wo => wo.ScheduledDate.HasValue && 
                    wo.ScheduledDate.Value.Date == today);
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
                "scheduleddate" => sortDescending 
                    ? query.OrderByDescending(wo => wo.ScheduledDate)
                    : query.OrderBy(wo => wo.ScheduledDate),
                "assignedtechnicianname" => sortDescending 
                    ? query.OrderByDescending(wo => wo.AssignedTechnicianName)
                    : query.OrderBy(wo => wo.AssignedTechnicianName),
                _ => sortDescending 
                    ? query.OrderByDescending(wo => wo.CreatedOn)
                    : query.OrderBy(wo => wo.CreatedOn)
            };

            // Get total count efficiently
            var totalCount = await query.CountAsync(cancellationToken);

            // Project to lightweight view objects
            var workOrderViews = await query
                .Select(wo => new
                {
                    wo.Id,
                    wo.WorkOrderNumber,
                    wo.Status,
                    wo.Priority,
                    wo.ServiceAddress,
                    AssignedTechnicianName = wo.AssignedTechnicianName ?? 
                        (wo.AssignedTechnician != null ? wo.AssignedTechnician.FirstName + " " + wo.AssignedTechnician.LastName : null),
                    wo.ScheduledDate,
                    CreatedAt = wo.CreatedOn,
                    WorkOrderTypeName = wo.WorkOrderType != null ? wo.WorkOrderType.Name : null,
                    CustomerName = (string?)null, // Will be populated from metadata if available
                    CustomerPhone = (string?)null,
                    IsOverdue = wo.ScheduledDate.HasValue && wo.ScheduledDate < DateTime.Today && 
                        wo.Status != "completed" && wo.Status != "cancelled",
                    DaysSinceCreated = (int)(DateTime.Today - wo.CreatedOn.Date).TotalDays,
                    StatusColor = wo.Status.ToLower() == "pending" ? "orange" :
                        wo.Status.ToLower() == "assigned" ? "blue" :
                        wo.Status.ToLower() == "in_progress" ? "purple" :
                        wo.Status.ToLower() == "completed" ? "green" :
                        wo.Status.ToLower() == "cancelled" ? "red" : "gray",
                    PriorityColor = wo.Priority.ToLower() == "high" ? "red" :
                        wo.Priority.ToLower() == "medium" ? "orange" :
                        wo.Priority.ToLower() == "low" ? "green" : "gray"
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (workOrderViews, totalCount);
        }

        // Get summary statistics for view/list
        public async Task<Dictionary<string, object>> GetWorkOrderViewSummaryAsync(
            CancellationToken cancellationToken = default)
        {
            var today = DateTime.Today;
            
            var summary = await _dbSet
                .AsNoTracking()
                .Where(wo => !wo.IsDeleted)
                .GroupBy(wo => 1)
                .Select(g => new
                {
                    TotalCount = g.Count(),
                    PendingCount = g.Count(wo => wo.Status == "pending"),
                    AssignedCount = g.Count(wo => wo.Status == "assigned"),
                    InProgressCount = g.Count(wo => wo.Status == "in_progress"),
                    CompletedCount = g.Count(wo => wo.Status == "completed"),
                    CancelledCount = g.Count(wo => wo.Status == "cancelled"),
                    HighPriorityCount = g.Count(wo => wo.Priority == "high"),
                    MediumPriorityCount = g.Count(wo => wo.Priority == "medium"),
                    LowPriorityCount = g.Count(wo => wo.Priority == "low"),
                    OverdueCount = g.Count(wo => wo.ScheduledDate.HasValue && wo.ScheduledDate < today && 
                        wo.Status != "completed" && wo.Status != "cancelled"),
                    TodayCount = g.Count(wo => wo.ScheduledDate.HasValue && wo.ScheduledDate.Value.Date == today),
                    ThisWeekCount = g.Count(wo => wo.ScheduledDate.HasValue && 
                        wo.ScheduledDate.Value.Date >= today.AddDays(-(int)today.DayOfWeek) && 
                        wo.ScheduledDate.Value.Date <= today.AddDays(6 - (int)today.DayOfWeek))
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (summary == null)
            {
                return new Dictionary<string, object>
                {
                    ["TotalCount"] = 0,
                    ["PendingCount"] = 0,
                    ["AssignedCount"] = 0,
                    ["InProgressCount"] = 0,
                    ["CompletedCount"] = 0,
                    ["CancelledCount"] = 0,
                    ["HighPriorityCount"] = 0,
                    ["MediumPriorityCount"] = 0,
                    ["LowPriorityCount"] = 0,
                    ["OverdueCount"] = 0,
                    ["TodayCount"] = 0,
                    ["ThisWeekCount"] = 0
                };
            }

            return new Dictionary<string, object>
            {
                ["TotalCount"] = summary.TotalCount,
                ["PendingCount"] = summary.PendingCount,
                ["AssignedCount"] = summary.AssignedCount,
                ["InProgressCount"] = summary.InProgressCount,
                ["CompletedCount"] = summary.CompletedCount,
                ["CancelledCount"] = summary.CancelledCount,
                ["HighPriorityCount"] = summary.HighPriorityCount,
                ["MediumPriorityCount"] = summary.MediumPriorityCount,
                ["LowPriorityCount"] = summary.LowPriorityCount,
                ["OverdueCount"] = summary.OverdueCount,
                ["TodayCount"] = summary.TodayCount,
                ["ThisWeekCount"] = summary.ThisWeekCount
            };
        }
    }
}
