using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowRule")]
    public class WorkflowRule : BaseEntity
    {
        [Required]
        public Guid WorkflowId { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow Workflow { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string RuleId { get; set; } = string.Empty; // Unique identifier within the workflow

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty; // assignment, visibility, validation, due_date, notification

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<WorkflowRuleCondition> Conditions { get; set; } = new List<WorkflowRuleCondition>();
        public virtual ICollection<WorkflowRuleAction> Actions { get; set; } = new List<WorkflowRuleAction>();
    }
}
