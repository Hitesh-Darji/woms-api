using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Location")]
    public class Location : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public LocationType Type { get; set; } = LocationType.Warehouse;

        [MaxLength(200)]
        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [Required]
        public LocationStatus Status { get; set; } = LocationStatus.Active;

        public Guid? ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public virtual ApplicationUser? Manager { get; set; }

        public Guid? ParentLocationId { get; set; }

        [ForeignKey(nameof(ParentLocationId))]
        public virtual Location? ParentLocation { get; set; }

        [MaxLength(100)]
        public string? Code { get; set; } // Location code for quick reference

        // Navigation properties
        public virtual ICollection<Location> SubLocations { get; set; } = new List<Location>();
        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; } = new List<InventoryLocation>();
        public virtual ICollection<InventoryTransaction> FromTransactions { get; set; } = new List<InventoryTransaction>();
        public virtual ICollection<InventoryTransaction> ToTransactions { get; set; } = new List<InventoryTransaction>();
        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}
