using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class Inventory : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? AdditionalId { get; set; }

        public int Allocated { get; set; } = 0;

        public int? AvailableQuantity { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Date { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ErrorMessage { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        public long? FileSize { get; set; }

        [MaxLength(100)]
        public string? FileType { get; set; }

        public int FinalValue { get; set; } = 0;

        [Required]
        [MaxLength(100)]
        public string Identifier { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LocationCode { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        [Required]
        [MaxLength(100)]
        public string SystemId { get; set; } = string.Empty;

        [Required]
        public UploadStatus UploadStatus { get; set; } = UploadStatus.Pending;

        [MaxLength(100)]
        public string? UploadedBy { get; set; }

        public int Value1 { get; set; } = 0;

        public int Value2 { get; set; } = 0;

        // Additional fields for enhanced inventory management
        [MaxLength(100)]
        public string? ModelName { get; set; } // e.g., "Electric Meter - 200A"

        [MaxLength(50)]
        public string? ModelCode { get; set; } // e.g., "MTR-001"

        [MaxLength(100)]
        public string? Manufacturer { get; set; } // e.g., "ElectriCorp"

        public AssetStatus? CurrentStatus { get; set; } = AssetStatus.Available;

        public DispositionStatus? DispositionStatus { get; set; } = Enums.DispositionStatus.None;

        public DateTime? InstallDate { get; set; }

        public DateTime? WarrantyExpirationDate { get; set; }

        public DateTime? RemovalDate { get; set; }

        [MaxLength(100)]
        public string? SerialNumber { get; set; }

        [MaxLength(100)]
        public string? PartNumber { get; set; }

        [MaxLength(100)]
        public string? Barcode { get; set; }

        [MaxLength(100)]
        public string? QRCode { get; set; }

        public InventoryCategory? Category { get; set; }

        public UnitOfMeasure? UnitOfMeasure { get; set; } = Enums.UnitOfMeasure.Each;

        public decimal? UnitCost { get; set; }

        public decimal? TotalValue { get; set; }

        public int? MinimumStockLevel { get; set; }

        public int? MaximumStockLevel { get; set; }

        public int? ReorderPoint { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Column(TypeName = "json")]
        public string? Attributes { get; set; } // JSON for additional item-specific attributes

        // Navigation properties
        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; } = new List<InventoryLocation>();
        public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
        public virtual ICollection<CycleCountItem> CycleCountItems { get; set; } = new List<CycleCountItem>();
        public virtual ICollection<JobKitItem> JobKitItems { get; set; } = new List<JobKitItem>();
        public virtual ICollection<InventoryDisposition> InventoryDispositions { get; set; } = new List<InventoryDisposition>();
    }
}
