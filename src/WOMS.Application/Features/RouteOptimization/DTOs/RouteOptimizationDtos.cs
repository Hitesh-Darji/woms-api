namespace WOMS.Application.Features.RouteOptimization.DTOs
{
    public class RouteOptimizationMetricsDto
    {
        public decimal AverageEfficiency { get; set; }
        public decimal TotalDistance { get; set; }
        public decimal TotalTime { get; set; }
        public int TotalStops { get; set; }
        public int TotalRoutes { get; set; }
        public int OptimizedRoutes { get; set; }
        public int PendingRoutes { get; set; }
    }

    public class TechnicianRouteDto
    {
        public Guid RouteId { get; set; }
        public string TechnicianName { get; set; } = string.Empty;
        public string TechnicianId { get; set; } = string.Empty;
        public decimal TotalDistance { get; set; }
        public decimal TotalTime { get; set; }
        public int TotalStops { get; set; }
        public decimal Efficiency { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Constraints { get; set; }
        public List<WorkOrderAssignmentDto> WorkOrders { get; set; } = new List<WorkOrderAssignmentDto>();
    }

    public class WorkOrderAssignmentDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public decimal EstimatedDuration { get; set; }
        public DateTime? ScheduledStartTime { get; set; }
        public DateTime? ScheduledEndTime { get; set; }
        public string? TimeWindow { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string? Equipment { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class TechnicianRoutesResponse
    {
        public List<TechnicianRouteDto> Routes { get; set; } = new List<TechnicianRouteDto>();
        public int TotalCount { get; set; }
    }


    public class OptimizeAllRoutesRequest
    {
        public DateTime Date { get; set; }
        public bool ForceReoptimization { get; set; } = false;
    }

    public class OptimizeAllRoutesResponse
    {
        public int RoutesOptimized { get; set; }
        public int RoutesSkipped { get; set; }
        public decimal AverageEfficiencyImprovement { get; set; }
        public decimal TotalDistanceReduction { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }

    public class ReorderWorkOrdersRequest
    {
        public Guid RouteId { get; set; }
        public Guid WorkOrderId { get; set; }
        public ReorderDirection Direction { get; set; }
    }

    public enum ReorderDirection
    {
        Up = 0,
        Down = 1
    }

    public class ReorderWorkOrdersResponse
    {
        public Guid RouteId { get; set; }
        public List<WorkOrderSequenceDto> WorkOrders { get; set; } = new();
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class WorkOrderSequenceDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Sequence { get; set; }
        public string TimeWindow { get; set; } = string.Empty;
        public string EstimatedDuration { get; set; } = string.Empty;
        public string EquipmentRequired { get; set; } = string.Empty;
    }
}
