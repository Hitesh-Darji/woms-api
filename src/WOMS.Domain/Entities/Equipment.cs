using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Equipment")]
    public class Equipment : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        public EquipmentCategory? Category { get; set; }

        public EquipmentStatus? Status { get; set; } = EquipmentStatus.Available;

        public bool IsRequired { get; set; } = false;

        [MaxLength(100)]
        public string? SerialNumber { get; set; }

        public DateTime? LastMaintenanceDate { get; set; }

        public DateTime? NextMaintenanceDate { get; set; }

        // Navigation properties
        public virtual ICollection<TechnicianEquipment> TechnicianEquipments { get; set; } = new List<TechnicianEquipment>();
        public virtual ICollection<WorkOrderEquipmentRequirement> WorkOrderEquipmentRequirements { get; set; } = new List<WorkOrderEquipmentRequirement>();
    }
}
