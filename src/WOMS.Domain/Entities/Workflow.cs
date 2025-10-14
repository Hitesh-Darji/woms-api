using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Workflow")]
    public class Workflow : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Required]
        public WorkflowCategory Category { get; set; } = WorkflowCategory.General;

        [Required]
        public int CurrentVersion { get; set; } = 1;

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<WorkflowNode> Nodes { get; set; } = new List<WorkflowNode>();
        public virtual ICollection<WorkflowStatus> Statuses { get; set; } = new List<WorkflowStatus>();
        public virtual ICollection<WorkflowAction> Actions { get; set; } = new List<WorkflowAction>();
        public virtual ICollection<WorkflowApproval> Approvals { get; set; } = new List<WorkflowApproval>();
        public virtual ICollection<WorkflowNotification> Notifications { get; set; } = new List<WorkflowNotification>();
        public virtual ICollection<WorkflowVersion> Versions { get; set; } = new List<WorkflowVersion>();
        public virtual ICollection<WorkflowInstance> Instances { get; set; } = new List<WorkflowInstance>();
    }
}
