using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("CountItem")]
    public class CountItem : BaseEntity
    {
        [Required]
        public Guid CycleCountId { get; set; }

        [ForeignKey(nameof(CycleCountId))]
        public virtual CycleCount CycleCount { get; set; } = null!;

        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual InventoryItem Item { get; set; } = null!;

        [Required]
        public int SystemQuantity { get; set; }

        [Required]
        public int CountedQuantity { get; set; }

        [Required]
        public int Variance { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Justification { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? SerialNumbers { get; set; } // JSON array as string

        [Required]
        public int OrderIndex { get; set; }
    }
}
