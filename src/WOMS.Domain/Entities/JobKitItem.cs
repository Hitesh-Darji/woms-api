using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("JobKitItem")]
    public class JobKitItem : BaseEntity
    {
        [Required]
        public Guid JobKitId { get; set; }

        [ForeignKey(nameof(JobKitId))]
        public virtual JobKit JobKit { get; set; } = null!;

        [Required]
        public Guid InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory Inventory { get; set; } = null!;

        [Required]
        public int Quantity { get; set; } = 1;

        public bool IsOptional { get; set; } = false;
    }
}
