using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("InventoryLocation")]
    public class InventoryLocation : BaseEntity
    {
        [Required]
        public Guid InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory Inventory { get; set; } = null!;

        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; } = null!;

        [Required]
        public int AvailableQuantity { get; set; } = 0;

        [Required]
        public int AllocatedQuantity { get; set; } = 0;

        [Required]
        public int ReservedQuantity { get; set; } = 0;

        public int TotalQuantity => AvailableQuantity + AllocatedQuantity + ReservedQuantity;

        public int? MinimumStockLevel { get; set; }

        public int? MaximumStockLevel { get; set; }

        public int? ReorderPoint { get; set; }

        [Required]
        public InventoryLocationStatus Status { get; set; } = InventoryLocationStatus.Active;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public DateTime? LastCountedDate { get; set; }

        public DateTime? LastMovementDate { get; set; }
    }
}
