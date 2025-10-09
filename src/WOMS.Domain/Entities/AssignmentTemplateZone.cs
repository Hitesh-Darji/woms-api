using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class AssignmentTemplateZone : BaseEntity
    {
        [Required]
        public Guid AssignmentTemplateId { get; set; }

        [ForeignKey(nameof(AssignmentTemplateId))]
        public virtual AssignmentTemplate AssignmentTemplate { get; set; } = null!;

        [Required]
        public Guid ZoneId { get; set; }

        [ForeignKey(nameof(ZoneId))]
        public virtual Zone Zone { get; set; } = null!;
    }
}
