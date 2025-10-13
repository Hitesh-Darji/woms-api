using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("ScanLog")]
    public class ScanLog : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string ScannedCode { get; set; } = string.Empty; // Barcode or QR code value

        [Required]
        public ScanType ScanType { get; set; } = ScanType.Barcode;

        [MaxLength(50)]
        public string? Result { get; set; } // Success, Not Found, Error

        public Guid? InventoryId { get; set; }

        [ForeignKey(nameof(InventoryId))]
        public virtual Inventory? Inventory { get; set; }

        public Guid? AssetId { get; set; }

        [ForeignKey(nameof(AssetId))]
        public virtual Asset? Asset { get; set; }

        [Required]
        public string ScannedByUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ScannedByUserId))]
        public virtual ApplicationUser ScannedByUser { get; set; } = null!;

        [MaxLength(200)]
        public string? DeviceInfo { get; set; } // Device type, OS, etc.

        [MaxLength(100)]
        public string? Location { get; set; } // GPS coordinates or location name

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Metadata { get; set; } // JSON for additional scan data
    }
}
