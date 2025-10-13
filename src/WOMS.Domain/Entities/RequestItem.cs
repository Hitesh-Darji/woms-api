using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("RequestItem")]
    public class RequestItem : BaseEntity
    {
        [Required]
        public Guid RequestId { get; set; }

        [ForeignKey(nameof(RequestId))]
        public virtual StockRequest Request { get; set; } = null!;

        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual InventoryItem Item { get; set; } = null!;

        [Required]
        public int RequestedQuantity { get; set; }

        public int? ApprovedQuantity { get; set; }

        public int? FulfilledQuantity { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        [Required]
        public int OrderIndex { get; set; }
    }
}
