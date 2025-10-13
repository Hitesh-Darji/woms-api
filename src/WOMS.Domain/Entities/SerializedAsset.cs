using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("SerializedAsset")]
    public class SerializedAsset : BaseEntity
    {
        [Required]
        public Guid ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual InventoryItem Item { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Model { get; set; } = string.Empty;

        public DateTime? WarrantyExpiration { get; set; }

        [Required]
        [MaxLength(20)]
        public AssetStatus Status { get; set; } = AssetStatus.Available;

        public Guid? CurrentLocationId { get; set; }

        [ForeignKey(nameof(CurrentLocationId))]
        public virtual Location? CurrentLocation { get; set; }

        public DateTime? InstallationDate { get; set; }

        public DateTime? RemovalDate { get; set; }

        [MaxLength(20)]
        public DispositionStatus? DispositionStatus { get; set; } 

        public DateTime? CalibrationDue { get; set; }

        public DateTime? LastServiceDate { get; set; }

        // Navigation properties
        public virtual ICollection<AssetHistory> AssetHistories { get; set; } = new List<AssetHistory>();
    }
}
