using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("BillingSchedule")]
    public class BillingSchedule : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public BillingScheduleFrequency Frequency { get; set; } = BillingScheduleFrequency.Monthly;

        public int? DayOfWeek { get; set; } // 0-6

        public int? DayOfMonth { get; set; } // 1-31

        [Required]
        [MaxLength(10)]
        public string Time { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? TemplateIds { get; set; } // JSON array as string

        [Required]
        public bool IsActive { get; set; } = true;
    }
}