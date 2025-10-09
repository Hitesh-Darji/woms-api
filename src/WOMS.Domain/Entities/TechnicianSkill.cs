using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class TechnicianSkill : BaseEntity
    {
        [Required]
        public Guid TechnicianId { get; set; }

        [ForeignKey(nameof(TechnicianId))]
        public virtual ApplicationUser Technician { get; set; } = null!;

        [Required]
        public Guid SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; } = null!;

        [MaxLength(20)]
        public string? Level { get; set; } // Basic, Intermediate, Advanced, Expert

        public DateTime? CertifiedDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [MaxLength(200)]
        public string? CertificationNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
