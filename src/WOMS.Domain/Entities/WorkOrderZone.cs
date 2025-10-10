using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrderZone")]
    public class WorkOrderZone : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        public Guid ZoneId { get; set; }

        [ForeignKey(nameof(ZoneId))]
        public virtual Zone Zone { get; set; } = null!;

        [MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active, Inactive

        public bool IsPrimary { get; set; } = false; // Primary zone for the work order
    }
}
