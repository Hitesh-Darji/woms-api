using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class AssignmentTemplateTechnician : BaseEntity
    {
        [Required]
        public Guid AssignmentTemplateId { get; set; }

        [ForeignKey(nameof(AssignmentTemplateId))]
        public virtual AssignmentTemplate AssignmentTemplate { get; set; } = null!;

        [Required]
        public Guid TechnicianId { get; set; }

        [ForeignKey(nameof(TechnicianId))]
        public virtual ApplicationUser Technician { get; set; } = null!;

        [MaxLength(50)]
        public string Status { get; set; } = "Preferred"; // Preferred, Required, Excluded

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
