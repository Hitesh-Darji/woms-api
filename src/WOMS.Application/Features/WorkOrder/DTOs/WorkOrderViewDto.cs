using System;

namespace WOMS.Application.Features.WorkOrder.DTOs
{
    /// <summary>
    /// Lightweight DTO for WorkOrder view/list operations
    /// Optimized for dropdowns, quick lists, and UI components
    /// </summary>
    public class WorkOrderViewDto
    {
        public Guid Id { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string ServiceAddress { get; set; } = string.Empty;
        public string? AssignedTechnicianName { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? WorkOrderTypeName { get; set; }
        
        // Additional fields for enhanced list views
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public bool IsOverdue { get; set; }
        public int DaysSinceCreated { get; set; }
        public string StatusColor { get; set; } = string.Empty; // For UI styling
        public string PriorityColor { get; set; } = string.Empty; // For UI styling
    }

    /// <summary>
    /// Response wrapper for WorkOrder view/list operations
    /// </summary>
    public class WorkOrderViewListResponse
    {
        public List<WorkOrderViewDto> WorkOrders { get; set; } = new List<WorkOrderViewDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
        
        // Additional metadata for UI
        public Dictionary<string, int> StatusCounts { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> PriorityCounts { get; set; } = new Dictionary<string, int>();
        public int OverdueCount { get; set; }
        public int TodayCount { get; set; }
    }
}
