using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Asset")]
    public class Asset : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Model { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ModelCode { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Manufacturer { get; set; }

        public InventoryCategory? Category { get; set; }

        [Required]
        public AssetStatus Status { get; set; } = AssetStatus.Available;

        [Required]
        public DispositionStatus DispositionStatus { get; set; } = DispositionStatus.None;

        public DateTime? InstallDate { get; set; }

        public DateTime? WarrantyExpirationDate { get; set; }

        public DateTime? RemovalDate { get; set; }

        public Guid? CurrentLocationId { get; set; }

        [ForeignKey(nameof(CurrentLocationId))]
        public virtual Location? CurrentLocation { get; set; }

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        [MaxLength(100)]
        public string? PartNumber { get; set; }

        [MaxLength(100)]
        public string? Barcode { get; set; }

        [MaxLength(100)]
        public string? QRCode { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Attributes { get; set; } // JSON for additional asset-specific attributes

        // Navigation properties
        public virtual ICollection<AssetHistory> AssetHistories { get; set; } = new List<AssetHistory>();
        public virtual ICollection<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
    }
}
