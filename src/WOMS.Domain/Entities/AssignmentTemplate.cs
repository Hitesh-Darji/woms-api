using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class AssignmentTemplate : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public TimeSpan StartTime { get; set; } = new TimeSpan(9, 0, 0); // 09:00

        public TimeSpan EndTime { get; set; } = new TimeSpan(17, 0, 0); // 17:00

        [Column(TypeName = "json")]
        public string? DaysOfWeek { get; set; } // JSON array of selected days

        // Auto-Assignment Rules
        public bool SkillMatchRequired { get; set; } = true;

        public bool WorkloadBalance { get; set; } = true;

        public bool LocationProximity { get; set; } = true;

        public bool AvailabilityCheck { get; set; } = true;

        public int UsageCount { get; set; } = 0;

        public DateTime? LastUsedDate { get; set; }

        [Column(TypeName = "json")]
        public string? Settings { get; set; } // JSON for additional template settings

        // Navigation properties
        public virtual ICollection<AssignmentTemplateWorkType> AssignmentTemplateWorkTypes { get; set; } = new List<AssignmentTemplateWorkType>();
        public virtual ICollection<AssignmentTemplateZone> AssignmentTemplateZones { get; set; } = new List<AssignmentTemplateZone>();
        public virtual ICollection<AssignmentTemplateSkill> AssignmentTemplateSkills { get; set; } = new List<AssignmentTemplateSkill>();
        public virtual ICollection<AssignmentTemplateTechnician> AssignmentTemplateTechnicians { get; set; } = new List<AssignmentTemplateTechnician>();
    }
}
