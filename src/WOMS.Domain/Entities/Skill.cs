using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Skill")]
    public class Skill : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; } // e.g., "Technical", "Safety", "Certification"

        public bool IsRequired { get; set; } = false;

        [MaxLength(20)]
        public string? Level { get; set; } // e.g., "Basic", "Intermediate", "Advanced"

        // Navigation properties
        public virtual ICollection<TechnicianSkill> TechnicianSkills { get; set; } = new List<TechnicianSkill>();
        public virtual ICollection<WorkOrderSkillRequirement> WorkOrderSkillRequirements { get; set; } = new List<WorkOrderSkillRequirement>();
    }
}
