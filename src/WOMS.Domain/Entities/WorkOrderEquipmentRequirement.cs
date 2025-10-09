using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkOrderEquipmentRequirement : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        public Guid EquipmentId { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public virtual Equipment Equipment { get; set; } = null!;

        public int Quantity { get; set; } = 1;

        public bool IsMandatory { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
