using System.ComponentModel.DataAnnotations;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Workflow.DTOs
{
    public class WorkflowDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public WorkflowCategory Category { get; set; } = WorkflowCategory.General;
        public int CurrentVersion { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public List<WorkflowNodeDto> Nodes { get; set; } = new List<WorkflowNodeDto>();
    }

    public class WorkflowNodeDto
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public WorkflowNodeType Type { get; set; } = WorkflowNodeType.Start;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public List<string>? Connections { get; set; }
        public int OrderIndex { get; set; }
        
        // Node-specific configurations
        public StartNodeConfigDto? StartConfig { get; set; }
        public StatusNodeConfigDto? StatusConfig { get; set; }
        public ConditionNodeConfigDto? ConditionConfig { get; set; }
        public ApprovalNodeConfigDto? ApprovalConfig { get; set; }
        public NotificationNodeConfigDto? NotificationConfig { get; set; }
        public EscalationNodeConfigDto? EscalationConfig { get; set; }
        public EndNodeConfigDto? EndConfig { get; set; }
    }

    public class NodePositionDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class CreateWorkflowRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public WorkflowCategory Category { get; set; } = WorkflowCategory.General;
        
        public List<WorkflowNodeDto>? Nodes { get; set; }
    }

    public class UpdateWorkflowRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public WorkflowCategory Category { get; set; } = WorkflowCategory.General;

        public bool IsActive { get; set; } = true;
        
        public List<WorkflowNodeDto> Nodes { get; set; } = new List<WorkflowNodeDto>();
    }

    public class AddNodeRequest
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [Required]
        public WorkflowNodeType Type { get; set; } = WorkflowNodeType.Start;

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public int OrderIndex { get; set; }
    }

    public class UpdateNodeRequest
    {
        [Required]
        public Guid NodeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public int OrderIndex { get; set; }
    }

    public class ConnectNodesRequest
    {
        [Required]
        public Guid FromNodeId { get; set; }

        [Required]
        public Guid ToNodeId { get; set; }
    }

    public class DisconnectNodesRequest
    {
        [Required]
        public Guid FromNodeId { get; set; }

        [Required]
        public Guid ToNodeId { get; set; }
    }

    public class TestWorkflowRequest
    {
        [Required]
        public Guid WorkflowId { get; set; }

        public Dictionary<string, object>? TestData { get; set; }
    }

    public class WorkflowListResponse
    {
        public List<WorkflowDto> Workflows { get; set; } = new List<WorkflowDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }

    public class WorkflowTestResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> ExecutionSteps { get; set; } = new List<string>();
        public Dictionary<string, object>? ResultData { get; set; }
    }

    // DTO specifically for GET operations that returns category as string
    public class WorkflowGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = "General";
        public int CurrentVersion { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public List<WorkflowNodeDto> Nodes { get; set; } = new List<WorkflowNodeDto>();
    }

    public class WorkflowListGetResponse
    {
        public List<WorkflowGetDto> Workflows { get; set; } = new List<WorkflowGetDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPreviousPage => PageNumber > 1;
    }

    public class NodeTypeInfoDto
    {
        public WorkflowNodeType Type { get; set; } = WorkflowNodeType.Start;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }

    // Node-specific configuration DTOs
    public class StartNodeConfigDto
    {
        public WorkflowTriggerType TriggerType { get; set; } = WorkflowTriggerType.ManualStart;
    }

    public class StatusNodeConfigDto
    {
        public string TargetStatus { get; set; } = string.Empty; // Planned, Dispatched, In Progress, etc.
        public WorkflowAssigneeType AutoAssignTo { get; set; } = WorkflowAssigneeType.KeepCurrentAssignee;
    }

    public class ConditionNodeConfigDto
    {
        public WorkflowConditionField FieldToCheck { get; set; } = WorkflowConditionField.Priority;
        public WorkflowConditionOperator Operator { get; set; } = WorkflowConditionOperator.Equals;
        public string Value { get; set; } = string.Empty;
        public string LogicalOperator { get; set; } = "AND"; // AND, OR for multiple conditions
    }

    public class ApprovalNodeConfigDto
    {
        public string ApprovalName { get; set; } = string.Empty; // e.g., Manager Approval
        public List<string> ApproverRoles { get; set; } = new List<string>(); // Supervisor, Manager, Safety Officer, etc.
        public WorkflowApprovalType ApprovalType { get; set; } = WorkflowApprovalType.SingleApprover;
        public int DeadlineHours { get; set; } = 24;
    }

    public class NotificationNodeConfigDto
    {
        public WorkflowNotificationType NotificationType { get; set; } = WorkflowNotificationType.Email;
        public WorkflowRecipientType Recipient { get; set; } = WorkflowRecipientType.CurrentAssignee;
        public WorkflowMessageTemplate MessageTemplate { get; set; } = WorkflowMessageTemplate.StatusChangeAlert;
    }

    public class EscalationNodeConfigDto
    {
        public WorkflowEscalationTrigger Trigger { get; set; } = WorkflowEscalationTrigger.TimeElapsed;
        public int HoursToWait { get; set; } = 24;
        public WorkflowEscalationAction Action { get; set; } = WorkflowEscalationAction.SendNotification;
    }

    public class EndNodeConfigDto
    {
        public WorkflowCompletionAction CompletionAction { get; set; } = WorkflowCompletionAction.NoAdditionalAction;
    }
}
