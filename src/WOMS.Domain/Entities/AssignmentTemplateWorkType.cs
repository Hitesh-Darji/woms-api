using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("AssignmentTemplateWorkType")]
    public class AssignmentTemplateWorkType : BaseEntity
    {
        [Required]
        public Guid AssignmentTemplateId { get; set; }

        [ForeignKey(nameof(AssignmentTemplateId))]
        public virtual AssignmentTemplate AssignmentTemplate { get; set; } = null!;

        [Required]
        public Guid WorkOrderTypeId { get; set; }

        [ForeignKey(nameof(WorkOrderTypeId))]
        public virtual WorkOrderType WorkOrderType { get; set; } = null!;
    }
}
