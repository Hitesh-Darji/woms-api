using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("CycleCountItem")]
    public class CycleCountItem : BaseEntity
    {
        [Required]
        public Guid CycleCountId { get; set; }

        [ForeignKey(nameof(CycleCountId))]
        public virtual CycleCount CycleCount { get; set; } = null!;

        [Required]
        public Guid InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory Inventory { get; set; } = null!;

        [Required]
        public int SystemQuantity { get; set; }

        [Required]
        public int CountedQuantity { get; set; }

        public int Variance => CountedQuantity - SystemQuantity;

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Required]
        public CycleCountItemStatus Status { get; set; } = CycleCountItemStatus.Counted;

        public bool IsVariance => Variance != 0;
    }
}
