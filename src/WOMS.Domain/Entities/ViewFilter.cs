using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class ViewFilter : BaseEntity
    {
        [Required]
        public Guid ViewId { get; set; }

        [ForeignKey(nameof(ViewId))]
        public virtual View View { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ColumnName { get; set; } = string.Empty; // Column to filter on

        [Required]
        [MaxLength(50)]
        public string Operator { get; set; } = string.Empty; // e.g., "equals", "contains", "greater_than", "less_than"

        [MaxLength(500)]
        public string? Value { get; set; } // Filter value

        [MaxLength(500)]
        public string? Value2 { get; set; } // For range filters (between, not between)

        public int Order { get; set; } = 0; // Order of filters

        public bool IsActive { get; set; } = true;

        [MaxLength(100)]
        public string? LogicalOperator { get; set; } = "AND"; // AND, OR for combining filters
    }
}
