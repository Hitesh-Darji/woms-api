using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkflowRuleAction : BaseEntity
    {
        [Required]
        public Guid RuleId { get; set; }

        [ForeignKey(nameof(RuleId))]
        public virtual WorkflowRule Rule { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ActionId { get; set; } = string.Empty; // Unique identifier within the rule

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // assign, set_due_date, show_field, hide_field, set_status, send_notification

        [Column(TypeName = "json")]
        public string? Config { get; set; } // JSON configuration for the action

        [Required]
        public int SortOrder { get; set; }
    }
}
