using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Location")]
    public class Location : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public LocationType Type { get; set; } = LocationType.Warehouse;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Manager { get; set; } = string.Empty;

        public Guid? ParentLocationId { get; set; }

        [ForeignKey(nameof(ParentLocationId))]
        public virtual Location? ParentLocation { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Location> SubLocations { get; set; } = new List<Location>();
        public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        public virtual ICollection<SerializedAsset> SerializedAssets { get; set; } = new List<SerializedAsset>();
        public virtual ICollection<StockTransaction> FromTransactions { get; set; } = new List<StockTransaction>();
        public virtual ICollection<StockTransaction> ToTransactions { get; set; } = new List<StockTransaction>();
        public virtual ICollection<StockRequest> FromRequests { get; set; } = new List<StockRequest>();
        public virtual ICollection<StockRequest> ToRequests { get; set; } = new List<StockRequest>();
        public virtual ICollection<CycleCount> CycleCounts { get; set; } = new List<CycleCount>();
    }
}
