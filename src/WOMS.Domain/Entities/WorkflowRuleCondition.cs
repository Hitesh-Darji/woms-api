using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowRuleCondition")]
    public class WorkflowRuleCondition : BaseEntity
    {
        [Required]
        public Guid RuleId { get; set; }

        [ForeignKey(nameof(RuleId))]
        public virtual WorkflowRule Rule { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ConditionId { get; set; } = string.Empty; // Unique identifier within the rule

        [Required]
        [MaxLength(100)]
        public string Field { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Operator { get; set; } = string.Empty; // equals, not_equals, contains, greater_than, less_than, in, not_in

        [Column(TypeName = "nvarchar(max)")]
        public string? Value { get; set; } // JSON value for the condition

        [MaxLength(10)]
        public string? LogicalOperator { get; set; } = "AND"; // and, or

        [Required]
        public int SortOrder { get; set; }
    }
}
