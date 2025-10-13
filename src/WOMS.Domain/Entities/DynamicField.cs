using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("DynamicField")]
    public class DynamicField : BaseEntity
    {
        [Required]
        public Guid TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual BillingTemplate Template { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Label { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Source { get; set; } = string.Empty; // work_order, asset, technician_time, completion_metrics

        [Required]
        [MaxLength(100)]
        public string Field { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Format { get; set; }

        [Required]
        public int OrderIndex { get; set; }
    }
}
