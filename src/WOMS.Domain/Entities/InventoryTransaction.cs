using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("InventoryTransaction")]
    public class InventoryTransaction : BaseEntity
    {
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Required]
        public InventoryTransactionType TransactionType { get; set; } = InventoryTransactionType.Issue;

        [Required]
        public Guid InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory Inventory { get; set; } = null!;

        public Guid? FromLocationId { get; set; }

        [ForeignKey(nameof(FromLocationId))]
        public virtual Location? FromLocation { get; set; }

        public Guid? ToLocationId { get; set; }

        [ForeignKey(nameof(ToLocationId))]
        public virtual Location? ToLocation { get; set; }

        [Required]
        public int Quantity { get; set; }

        public decimal? UnitCost { get; set; }

        public decimal? TotalCost { get; set; }

        [MaxLength(200)]
        public string? Reference { get; set; } // Work Order ID, PO Number, etc.

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Completed;
    }
}
