using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("TechnicianEquipment")]
    public class TechnicianEquipment : BaseEntity
    {
        [Required]
        [MaxLength(450)]
        public string TechnicianId { get; set; } = string.Empty;

        [ForeignKey(nameof(TechnicianId))]
        public virtual ApplicationUser Technician { get; set; } = null!;

        [Required]
        public Guid EquipmentId { get; set; }

        [ForeignKey(nameof(EquipmentId))]
        public virtual Equipment Equipment { get; set; } = null!;

        public DateTime? AssignedDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Assigned"; // Assigned, In Use, Returned, Lost, Damaged

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
