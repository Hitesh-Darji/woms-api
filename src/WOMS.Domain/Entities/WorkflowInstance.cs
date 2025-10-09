using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkflowInstance : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [MaxLength(200)]
        public string? InstanceName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "running"; // running, completed, failed, paused, cancelled

        [MaxLength(100)]
        public string? CurrentNodeId { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CompletedAt { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }

        [Column(TypeName = "json")]
        public string? Metadata { get; set; } // JSON metadata for additional data

        // Navigation properties
        public virtual ICollection<WorkflowInstanceStep> Steps { get; set; } = new List<WorkflowInstanceStep>();
    }
}
