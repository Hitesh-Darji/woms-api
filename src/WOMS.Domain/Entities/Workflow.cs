using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Workflow")]
    public class Workflow : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsPublished { get; set; } = false;

        public bool IsLocked { get; set; } = false;

        public int Version { get; set; } = 1;

        [Column(TypeName = "json")]
        public string? Metadata { get; set; } // JSON string for additional data

        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }

        // Navigation properties
        public virtual ICollection<WorkflowNode> Nodes { get; set; } = new List<WorkflowNode>();
        public virtual ICollection<WorkflowEdge> Edges { get; set; } = new List<WorkflowEdge>();
        public virtual ICollection<WorkflowStatus> Statuses { get; set; } = new List<WorkflowStatus>();
        public virtual ICollection<WorkflowTransition> Transitions { get; set; } = new List<WorkflowTransition>();
        public virtual ICollection<WorkflowRule> Rules { get; set; } = new List<WorkflowRule>();
        public virtual ICollection<WorkflowAction> Actions { get; set; } = new List<WorkflowAction>();
        public virtual ICollection<WorkflowInstance> Instances { get; set; } = new List<WorkflowInstance>();
        public virtual ICollection<WorkflowForm> WorkflowForms { get; set; } = new List<WorkflowForm>();
    }
}
