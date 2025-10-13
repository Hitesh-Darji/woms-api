using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowEscalation")]
    public class WorkflowEscalation : BaseEntity
    {
        [Required]
        public Guid ApprovalId { get; set; }

        [ForeignKey(nameof(ApprovalId))]
        public virtual WorkflowApproval Approval { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Trigger { get; set; } = string.Empty; // time_elapsed, no_response, condition_met

        [Column(TypeName = "nvarchar(max)")]
        public string? TriggerValue { get; set; }

        [Required]
        [MaxLength(20)]
        public string Action { get; set; } = string.Empty; // reassign, notify, status_change, escalate_approval

        [Column(TypeName = "nvarchar(max)")]
        public string? ActionConfig { get; set; } // JSON as string

        [Required]
        public int OrderIndex { get; set; }
    }
}
