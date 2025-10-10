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
        [MaxLength(100)]
        public string NodeId { get; set; } = string.Empty; // Unique identifier within the workflow

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // trigger, action, condition, end, status, assignment, timer

        public double PositionX { get; set; }

        public double PositionY { get; set; }

        [Required]
        [MaxLength(200)]
        public string Label { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "json")]
        public string? Config { get; set; } // JSON configuration for the node

        // Navigation properties
        public virtual ICollection<WorkflowEdge> OutgoingEdges { get; set; } = new List<WorkflowEdge>();
        public virtual ICollection<WorkflowEdge> IncomingEdges { get; set; } = new List<WorkflowEdge>();
        public virtual ICollection<WorkflowAssignment> Assignments { get; set; } = new List<WorkflowAssignment>();
        public virtual ICollection<WorkflowInstanceStep> InstanceSteps { get; set; } = new List<WorkflowInstanceStep>();
    }
}
