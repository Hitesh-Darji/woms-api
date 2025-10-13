using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("KitItem")]
    public class KitItem : BaseEntity
    {
        [Required]
        public Guid JobKitId { get; set; }

        [ForeignKey(nameof(JobKitId))]
        public virtual JobKit JobKit { get; set; } = null!;

        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual InventoryItem Item { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool IsOptional { get; set; } = false;

        [Required]
        public int OrderIndex { get; set; }
    }
}
