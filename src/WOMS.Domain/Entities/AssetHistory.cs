using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("AssetHistory")]
    public class AssetHistory : BaseEntity
    {
        [Required]
        public Guid AssetId { get; set; }

        [ForeignKey(nameof(AssetId))]
        public virtual SerializedAsset Asset { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Action { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? FromLocation { get; set; }

        [MaxLength(255)]
        public string? ToLocation { get; set; }

        [Required]
        [MaxLength(255)]
        public string UserId { get; set; } = string.Empty;

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "nvarchar(max)")]
        public string? Details { get; set; }
    }
}
