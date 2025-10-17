using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrderAssignment")]
    public class WorkOrderAssignment : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string TechnicianId { get; set; } = string.Empty;

        [Required]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(255)]
        public string AssignedBy { get; set; } = string.Empty;

        [Required]
        public AssignmentStatus Status { get; set; } = AssignmentStatus.Assigned;

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }
    }
}