using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("AssignmentTemplateSkill")]
    public class AssignmentTemplateSkill : BaseEntity
    {
        [Required]
        public Guid AssignmentTemplateId { get; set; }

        [ForeignKey(nameof(AssignmentTemplateId))]
        public virtual AssignmentTemplate AssignmentTemplate { get; set; } = null!;

        [Required]
        public Guid SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; } = null!;

        [MaxLength(20)]
        public string? RequiredLevel { get; set; } // Basic, Intermediate, Advanced, Expert

        public bool IsMandatory { get; set; } = true;
    }
}
