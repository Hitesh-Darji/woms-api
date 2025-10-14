using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.Workflow.DTOs
{
    public class WorkflowDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty;
        public int CurrentVersion { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public List<WorkflowNodeDto> Nodes { get; set; } = new List<WorkflowNodeDto>();
    }

    public class WorkflowNodeDto
    {
        public Guid Id { get; set; }
        public Guid WorkflowId { get; set; }
        public string Type { get; set; } = string.Empty; // start, status, condition, approval, notification, escalation, end
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public NodePositionDto? Position { get; set; }
        public Dictionary<string, object>? Data { get; set; }
        public List<string>? Connections { get; set; }
        public int OrderIndex { get; set; }
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
        [MaxLength(100)]
        public string Category { get; set; } = "General";
    }

    public class UpdateWorkflowRequest
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = "General";

        public bool IsActive { get; set; } = true;
    }

    public class AddNodeRequest
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } = string.Empty; // start, status, condition, approval, notification, escalation, end

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

    public class NodeTypeInfoDto
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
