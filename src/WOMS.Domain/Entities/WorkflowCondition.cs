using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkflowCondition")]
    public class WorkflowCondition : BaseEntity
    {
        [Required]
        public Guid NodeId { get; set; }

        [ForeignKey(nameof(NodeId))]
        public virtual WorkflowNode Node { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Field { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Operator { get; set; } = string.Empty; // equals, not_equals, greater_than, less_than, contains, in, not_in

        [Column(TypeName = "nvarchar(max)")]
        public string? Value { get; set; }

        [MaxLength(5)]
        public string? LogicalOperator { get; set; } // AND, OR

        [Required]
        public int OrderIndex { get; set; }
    }
}
