using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowStatusTransition")]
    public class WorkflowStatusTransition : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        public Guid FromStatusId { get; set; }

        [ForeignKey(nameof(FromStatusId))]
        public virtual WorkflowStatus FromStatus { get; set; } = null!;

        [Required]
        public Guid ToStatusId { get; set; }

        [ForeignKey(nameof(ToStatusId))]
        public virtual WorkflowStatus ToStatus { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}