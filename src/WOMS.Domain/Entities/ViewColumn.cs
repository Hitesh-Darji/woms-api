using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class ViewColumn : BaseEntity
    {
        [Required]
        public Guid ViewId { get; set; }

        [ForeignKey(nameof(ViewId))]
        public virtual View View { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ColumnName { get; set; } = string.Empty; // e.g., "WorkOrderId", "Priority", "Status"

        [Required]
        [MaxLength(200)]
        public string DisplayName { get; set; } = string.Empty; // e.g., "Work Order ID", "Priority", "Status"

        [Required]
        public int Order { get; set; } // Display order of the column

        public bool IsVisible { get; set; } = true;

        public int Width { get; set; } = 150; // Column width in pixels

        [MaxLength(50)]
        public string? DataType { get; set; } // e.g., "string", "number", "date", "boolean"

        [MaxLength(100)]
        public string? Format { get; set; } // e.g., "currency", "percentage", "date-format"

        public bool IsSortable { get; set; } = true;

        public bool IsFilterable { get; set; } = true;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
