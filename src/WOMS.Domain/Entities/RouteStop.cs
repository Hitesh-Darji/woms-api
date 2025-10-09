using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class RouteStop : BaseEntity
    {
        [Required]
        public Guid RouteId { get; set; }

        [ForeignKey(nameof(RouteId))]
        public virtual Route Route { get; set; } = null!;

        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        public int SequenceNumber { get; set; }

        public decimal EstimatedDuration { get; set; } = 0; // in hours

        public DateTime? ScheduledStartTime { get; set; }

        public DateTime? ScheduledEndTime { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, In Progress, Completed, Skipped, Cancelled

        [MaxLength(500)]
        public string? Notes { get; set; }

        public decimal? TravelTime { get; set; } // in hours

        public decimal? Distance { get; set; } // in kilometers
    }
}
