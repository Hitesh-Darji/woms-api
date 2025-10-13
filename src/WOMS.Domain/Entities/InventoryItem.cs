using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("InventoryItem")]
    public class InventoryItem : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string PartNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string UnitOfMeasure { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal UnitCost { get; set; }

        [MaxLength(100)]
        public string? Barcode { get; set; }

        [MaxLength(100)]
        public string? QrCode { get; set; }

        [Required]
        public bool IsSerializedAsset { get; set; } = false;

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        public virtual ICollection<SerializedAsset> SerializedAssets { get; set; } = new List<SerializedAsset>();
        public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
        public virtual ICollection<RequestItem> RequestItems { get; set; } = new List<RequestItem>();
        public virtual ICollection<CountItem> CountItems { get; set; } = new List<CountItem>();
        public virtual ICollection<KitItem> KitItems { get; set; } = new List<KitItem>();
    }
}
