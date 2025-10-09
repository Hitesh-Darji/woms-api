using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkflowTransition : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string TransitionId { get; set; } = string.Empty; // Unique identifier within the workflow

        [Required]
        public Guid FromStatusId { get; set; }

        [ForeignKey(nameof(FromStatusId))]
        public virtual WorkflowStatus FromStatus { get; set; } = null!;

        [Required]
        public Guid ToStatusId { get; set; }

        [ForeignKey(nameof(ToStatusId))]
        public virtual WorkflowStatus ToStatus { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool RequiresValidation { get; set; } = false;

        // Ensure we don't have duplicate transitions
        public bool IsValid => FromStatusId != ToStatusId;
    }
}
