using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowInstanceStep")]
    public class WorkflowInstanceStep : BaseEntity
    {
        [Required]
        public Guid InstanceId { get; set; }

        [ForeignKey(nameof(InstanceId))]
        public virtual WorkflowInstance Instance { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string NodeId { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string StepName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "pending"; // pending, in_progress, completed, failed, skipped

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        [MaxLength(100)]
        public string? AssigneeId { get; set; }

        [MaxLength(200)]
        public string? AssigneeName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? ResultData { get; set; } // JSON result data

        [MaxLength(1000)]
        public string? ErrorMessage { get; set; }

        // Navigation properties
        [NotMapped]
        public virtual WorkflowNode? Node { get; set; }
    }
}
