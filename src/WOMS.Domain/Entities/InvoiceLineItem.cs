using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("InvoiceLineItem")]
    public class InvoiceLineItem : BaseEntity
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public virtual Invoice Invoice { get; set; } = null!;

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Rate { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Total { get; set; }

        public Guid? RateTableId { get; set; }

        [ForeignKey(nameof(RateTableId))]
        public virtual RateTable? RateTable { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        [MaxLength(255)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? WorkType { get; set; }

        [Required]
        public int OrderIndex { get; set; }
    }
}
