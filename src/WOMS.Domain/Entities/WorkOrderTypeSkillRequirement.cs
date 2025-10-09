using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class WorkOrderTypeSkillRequirement : BaseEntity
    {
        [Required]
        public Guid WorkOrderTypeId { get; set; }

        [ForeignKey(nameof(WorkOrderTypeId))]
        public virtual WorkOrderType WorkOrderType { get; set; } = null!;

        [Required]
        public Guid SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; } = null!;

        [MaxLength(20)]
        public string? RequiredLevel { get; set; } // Basic, Intermediate, Advanced, Expert

        public bool IsMandatory { get; set; } = true;
    }
}
