using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowInstance")]
    public class WorkflowInstance : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        public Guid? CurrentNodeId { get; set; }

        [ForeignKey(nameof(CurrentNodeId))]
        public virtual WorkflowNode? CurrentNode { get; set; }

        [Required]
        public WorkflowInstanceStatus Status { get; set; } = WorkflowInstanceStatus.Running;

        [Required]
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Data { get; set; } // JSON as string

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<WorkflowExecutionLog> ExecutionLogs { get; set; } = new List<WorkflowExecutionLog>();
    }
}
