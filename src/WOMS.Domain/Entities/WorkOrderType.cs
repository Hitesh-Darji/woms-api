using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkOrderType : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; } // e.g., "Installation", "Repair", "Maintenance", "Emergency"

        [Required]
        [MaxLength(20)]
        public string Priority { get; set; } = "Medium"; // Low, Medium, High, Critical

        public int EstimatedDurationMinutes { get; set; } = 0;

        public bool RequiresApproval { get; set; } = false;

        [Column(TypeName = "json")]
        public string? DefaultSettings { get; set; } // JSON for type-specific settings

        // Navigation properties
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public virtual ICollection<WorkOrderTypeSkillRequirement> WorkOrderTypeSkillRequirements { get; set; } = new List<WorkOrderTypeSkillRequirement>();
        public virtual ICollection<WorkOrderTypeEquipmentRequirement> WorkOrderTypeEquipmentRequirements { get; set; } = new List<WorkOrderTypeEquipmentRequirement>();
    }
}
