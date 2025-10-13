namespace WOMS.Application.Features.WorkOrder.DTOs
{
    public class WorkOrderDto
    {
        public Guid Id { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public Guid? WorkflowId { get; set; }
        public Guid? WorkOrderTypeId { get; set; }
        public string? WorkOrderTypeName { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ServiceAddress { get; set; } = string.Empty;
        public string? MeterNumber { get; set; }
        public int? CurrentReading { get; set; }
        public string? AssignedTechnicianId { get; set; }
        public string? AssignedTechnicianName { get; set; }
        public string? Notes { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Additional fields for the UI table
        public string? Utility { get; set; } 
        public string? Make { get; set; } 
        public string? Model { get; set; } 
        public string? Size { get; set; } 
        public string? Location { get; set; } 
        public DateTime? DueDate { get; set; } 
    }

    public class WorkOrderTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string Priority { get; set; } = string.Empty;
        public int EstimatedDurationMinutes { get; set; }
        public bool RequiresApproval { get; set; }
    }

    public class WorkOrderListResponse
    {
        public List<WorkOrderDto> WorkOrders { get; set; } = new List<WorkOrderDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }
}
