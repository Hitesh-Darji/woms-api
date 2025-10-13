using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Stock")]
    public class Stock : BaseEntity
    {
        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual InventoryItem Item { get; set; } = null!;

        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; } = null!;

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public int MinThreshold { get; set; } = 0;

        [Required]
        public int MaxThreshold { get; set; } = 0;

        [Required]
        public int Reserved { get; set; } = 0;

        [Required]
        public int OnHand { get; set; } = 0;

        [Required]
        public int InTransit { get; set; } = 0;
    }
}
