using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrderColumn")]
    public class WorkOrderColumn : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ColumnId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Label { get; set; } = string.Empty;

        public int? Width { get; set; }

        [Required]
        public bool Sortable { get; set; } = true;

        [Required]
        public bool Filterable { get; set; } = true;

        [Required]
        public int OrderIndex { get; set; }
    }
}
