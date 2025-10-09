using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class Zone : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Type { get; set; } // e.g., "Residential", "Commercial", "Industrial", "All Zones"

        [MaxLength(200)]
        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Radius { get; set; } // in kilometers

        [MaxLength(20)]
        public string? Status { get; set; } = "Active"; // Active, Inactive

        // Navigation properties
        public virtual ICollection<TechnicianZone> TechnicianZones { get; set; } = new List<TechnicianZone>();
        public virtual ICollection<WorkOrderZone> WorkOrderZones { get; set; } = new List<WorkOrderZone>();
    }
}
