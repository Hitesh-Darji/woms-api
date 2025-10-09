using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkOrderAssignment : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Identifier { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string SystemId { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public DateTime AllocatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ConsumedAt { get; set; }
    }
}
