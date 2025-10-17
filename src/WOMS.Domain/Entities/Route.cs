using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Route")]
    public class Route : BaseEntity
    {
        [Required]
        [MaxLength(450)]
        public string DriverId { get; set; } = string.Empty;

        [ForeignKey(nameof(DriverId))]
        public virtual ApplicationUser Driver { get; set; } = null!;

        [Required]
        public DateTime RouteDate { get; set; }

        public decimal TotalDistance { get; set; } = 0; // in kilometers

        public decimal TotalTime { get; set; } = 0; // in hours

        public int TotalStops { get; set; } = 0;

        public decimal Efficiency { get; set; } = 0; // percentage (0-100)

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Planned"; // Planned, Optimized, Dispatched, In Progress, Completed, Cancelled

        [Column(TypeName = "nvarchar(max)")]
        public string? Constraints { get; set; } // JSON for route constraints

        [Column(TypeName = "nvarchar(max)")]
        public string? OptimizationSettings { get; set; } // JSON for optimization settings

        public DateTime? DispatchedAt { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Navigation properties
        public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    }
}
