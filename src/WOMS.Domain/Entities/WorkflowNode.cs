using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowNode")]
    public class WorkflowNode : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } = string.Empty; // start, status, condition, approval, notification, escalation, end

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Position { get; set; } // JSON as string

        [Column(TypeName = "nvarchar(max)")]
        public string? Data { get; set; } // JSON as string

        [Column(TypeName = "nvarchar(max)")]
        public string? Connections { get; set; } // JSON array as string

        [Required]
        public int OrderIndex { get; set; }

        // Navigation properties
        public virtual ICollection<WorkflowCondition> Conditions { get; set; } = new List<WorkflowCondition>();
        public virtual ICollection<WorkflowExecutionLog> ExecutionLogs { get; set; } = new List<WorkflowExecutionLog>();
    }
}
