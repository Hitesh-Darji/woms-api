using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class BillingScheduleTemplate : BaseEntity
    {
        [Required]
        public Guid BillingScheduleId { get; set; }

        [ForeignKey(nameof(BillingScheduleId))]
        public virtual BillingSchedule BillingSchedule { get; set; } = null!;

        [Required]
        public Guid BillingTemplateId { get; set; }

        [ForeignKey(nameof(BillingTemplateId))]
        public virtual BillingTemplate BillingTemplate { get; set; } = null!;

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; } // Additional notes for this template in this schedule

        [Column(TypeName = "nvarchar(max)")]
        public string? TemplateSettings { get; set; } // JSON for template-specific settings in this schedule
    }
}
