using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("AssetHistory")]
    public class AssetHistory : BaseEntity
    {
        [Required]
        public Guid AssetId { get; set; }

        [ForeignKey(nameof(AssetId))]
        public virtual Asset Asset { get; set; } = null!;

        [Required]
        public AssetAction Action { get; set; } = AssetAction.Install;

        [MaxLength(200)]
        public string? Description { get; set; }

        public Guid? FromLocationId { get; set; }

        [ForeignKey(nameof(FromLocationId))]
        public virtual Location? FromLocation { get; set; }

        public Guid? ToLocationId { get; set; }

        [ForeignKey(nameof(ToLocationId))]
        public virtual Location? ToLocation { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Completed;

        [MaxLength(1000)]
        public string? Details { get; set; }

        [Column(TypeName = "json")]
        public string? Metadata { get; set; } // JSON for additional action-specific data
    }
}
