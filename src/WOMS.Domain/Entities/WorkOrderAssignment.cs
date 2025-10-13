using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [MaxLength(20)]
        public string Status { get; set; } = "assigned"; // assigned, accepted, rejected, completed

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }
    }
}