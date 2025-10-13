using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("AggregationRule")]
    public class AggregationRule : BaseEntity
    {
        [Required]
        public Guid TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual BillingTemplate Template { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string GroupBy { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string AggregateBy { get; set; } = string.Empty; // category, location, work_type, date_range

        [Required]
        public int OrderIndex { get; set; }
    }
}
