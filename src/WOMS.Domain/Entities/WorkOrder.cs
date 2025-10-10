using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrder")]
    public class WorkOrder : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string WorkOrderNumber { get; set; } = string.Empty;

        public Guid? WorkflowId { get; set; }

        public Guid? WorkOrderTypeId { get; set; }

        [ForeignKey(nameof(WorkOrderTypeId))]
        public virtual WorkOrderType? WorkOrderType { get; set; }

        [ForeignKey(nameof(WorkflowId))]
        public virtual Workflow? Workflow { get; set; }

        [Required]
        [MaxLength(20)]
        public string Priority { get; set; } = "medium"; // low, medium, high, urgent

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "pending";

        [Required]
        [MaxLength(500)]
        public string ServiceAddress { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? MeterNumber { get; set; }

        public int? CurrentReading { get; set; }

        public Guid? AssignedTechnicianId { get; set; }

        [ForeignKey(nameof(AssignedTechnicianId))]
        public virtual ApplicationUser? AssignedTechnician { get; set; }

        [MaxLength(200)]
        public string? AssignedTechnicianName { get; set; }

        [MaxLength(1000)]
        public string? Notes { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }

        [Column(TypeName = "json")]
        public string? Metadata { get; set; }

        // Navigation properties
        public virtual ICollection<WorkOrderAssignment> WorkOrderAssignments { get; set; } = new List<WorkOrderAssignment>();
        public virtual ICollection<WorkOrderAttachment> WorkOrderAttachments { get; set; } = new List<WorkOrderAttachment>();
        public virtual ICollection<WorkOrderSkillRequirement> WorkOrderSkillRequirements { get; set; } = new List<WorkOrderSkillRequirement>();
        public virtual ICollection<WorkOrderEquipmentRequirement> WorkOrderEquipmentRequirements { get; set; } = new List<WorkOrderEquipmentRequirement>();
        public virtual ICollection<WorkOrderZone> WorkOrderZones { get; set; } = new List<WorkOrderZone>();
        public virtual ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    }
}
