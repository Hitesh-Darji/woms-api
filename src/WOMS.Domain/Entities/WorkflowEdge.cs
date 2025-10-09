using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkflowEdge : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string EdgeId { get; set; } = string.Empty; // Unique identifier within the workflow

        [Required]
        [MaxLength(100)]
        public string SourceNodeId { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string TargetNodeId { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? EdgeType { get; set; }

        [MaxLength(200)]
        public string? Label { get; set; }

        // Navigation properties to nodes
        [NotMapped]
        public virtual WorkflowNode? SourceNode { get; set; }
        [NotMapped]
        public virtual WorkflowNode? TargetNode { get; set; }
    }
}
