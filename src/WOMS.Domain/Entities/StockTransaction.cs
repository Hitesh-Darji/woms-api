using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("StockTransaction")]
    public class StockTransaction : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public InventoryTransactionType Type { get; set; }
        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual InventoryItem Item { get; set; } = null!;

        public Guid? FromLocationId { get; set; }

        [ForeignKey(nameof(FromLocationId))]
        public virtual Location? FromLocation { get; set; }

        public Guid? ToLocationId { get; set; }

        [ForeignKey(nameof(ToLocationId))]
        public virtual Location? ToLocation { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal UnitCost { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserId { get; set; } = string.Empty;

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? SerialNumbers { get; set; } // JSON array as string

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        [MaxLength(255)]
        public string? ApprovedBy { get; set; }

        [MaxLength(255)]
        public string? Reference { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
