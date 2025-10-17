namespace WOMS.Application.Features.Assignment.DTOs
{
    public class UnassignedWorkOrderDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Description { get; set; }
        public List<string> RequiredSkills { get; set; } = new List<string>();
        public List<string> RequiredEquipment { get; set; } = new List<string>();
        public AssignmentRecommendationDto? Recommendation { get; set; }
        public List<TechnicianOptionDto> AvailableTechnicians { get; set; } = new List<TechnicianOptionDto>();
    }

    public class AssignmentRecommendationDto
    {
        public string TechnicianName { get; set; } = string.Empty;
        public string TechnicianId { get; set; } = string.Empty;
        public decimal MatchScore { get; set; }
        public string Reason { get; set; } = string.Empty;
        public List<string> MatchingSkills { get; set; } = new List<string>();
        public bool HasRequiredEquipment { get; set; }
        public decimal DistanceFromLocation { get; set; }
        public int CurrentWorkload { get; set; }
        public int MaxWorkload { get; set; }
    }

    public class TechnicianOptionDto
    {
        public string TechnicianId { get; set; } = string.Empty;
        public string TechnicianName { get; set; } = string.Empty;
        public int CurrentWorkload { get; set; }
        public int MaxWorkload { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> Equipment { get; set; } = new List<string>();
        public decimal MatchScore { get; set; }
    }

    public class UnassignedWorkOrdersResponse
    {
        public List<UnassignedWorkOrderDto> WorkOrders { get; set; } = new List<UnassignedWorkOrderDto>();
        public int TotalCount { get; set; }
        public int CriticalCount { get; set; }
        public int HighCount { get; set; }
        public int MediumCount { get; set; }
        public int LowCount { get; set; }
    }

    public class TechnicianStatusDto
    {
        public string TechnicianId { get; set; } = string.Empty;
        public string TechnicianName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // available, busy, break, offline
        public string Location { get; set; } = string.Empty;
        public int CurrentWorkload { get; set; }
        public int MaxWorkload { get; set; }
        public DateTime? ShiftEndTime { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> Equipment { get; set; } = new List<string>();
        public List<AssignedWorkOrderDto> AssignedWorkOrders { get; set; } = new List<AssignedWorkOrderDto>();
    }

    public class AssignedWorkOrderDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime? ScheduledStartTime { get; set; }
        public DateTime? ScheduledEndTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class TechnicianStatusResponse
    {
        public List<TechnicianStatusDto> Technicians { get; set; } = new List<TechnicianStatusDto>();
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
        public int BusyCount { get; set; }
        public int OnBreakCount { get; set; }
        public int OfflineCount { get; set; }
    }

    public class AssignWorkOrderRequest
    {
        public Guid WorkOrderId { get; set; }
        public string TechnicianId { get; set; } = string.Empty;
    }

    public class AutoAssignAllRequest
    {
        public bool ForceReassignment { get; set; } = false;
        public string? PriorityFilter { get; set; }
    }

    public class AutoAssignAllResponse
    {
        public int WorkOrdersAssigned { get; set; }
        public int WorkOrdersSkipped { get; set; }
        public int WorkOrdersFailed { get; set; }
        public List<AssignmentResultDto> AssignmentResults { get; set; } = new List<AssignmentResultDto>();
        public List<string> Messages { get; set; } = new List<string>();
    }

    public class AssignmentResultDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public string TechnicianId { get; set; } = string.Empty;
        public string TechnicianName { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string? Reason { get; set; }
        public decimal MatchScore { get; set; }
    }

    public class AssignmentRecommendationsDto
    {
        public Guid WorkOrderId { get; set; }
        public string WorkOrderNumber { get; set; } = string.Empty;
        public List<AssignmentRecommendationDto> Recommendations { get; set; } = new List<AssignmentRecommendationDto>();
        public List<TechnicianOptionDto> AllAvailableTechnicians { get; set; } = new List<TechnicianOptionDto>();
    }
}
