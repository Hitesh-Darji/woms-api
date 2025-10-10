using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrderSkillRequirement")]
    public class WorkOrderSkillRequirement : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        public Guid SkillId { get; set; }

        [ForeignKey(nameof(SkillId))]
        public virtual Skill Skill { get; set; } = null!;

        [MaxLength(20)]
        public string? RequiredLevel { get; set; } // Basic, Intermediate, Advanced, Expert

        public bool IsMandatory { get; set; } = true;
    }
}
