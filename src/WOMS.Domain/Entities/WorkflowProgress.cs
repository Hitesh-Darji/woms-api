using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowProgress")]
    public class WorkflowProgress : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        public Guid CurrentStatusId { get; set; }

        [ForeignKey(nameof(CurrentStatusId))]
        public virtual WorkflowStatus CurrentStatus { get; set; } = null!;

        [Column(TypeName = "json")]
        public string? CompletedSteps { get; set; } // JSON array of completed step IDs

        [Column(TypeName = "json")]
        public string? PendingSteps { get; set; } // JSON array of pending step IDs

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? AssigneeId { get; set; }

        [MaxLength(200)]
        public string? AssigneeName { get; set; }

        [Column(TypeName = "json")]
        public string? Metadata { get; set; } // JSON metadata for additional data
    }
}
