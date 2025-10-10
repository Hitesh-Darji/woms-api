using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class BillingSchedule : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string ScheduleName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Frequency { get; set; } = "Monthly"; // Monthly, Weekly, Daily, Quarterly, Yearly

        [Required]
        public TimeSpan Time { get; set; } = new TimeSpan(9, 0, 0); // 09:00

        public int? DayOfMonth { get; set; } // For monthly schedules (1-31)

        public int? DayOfWeek { get; set; } // For weekly schedules (0-6, Sunday = 0)

        [MaxLength(100)]
        public string? DayOfWeekName { get; set; } // Monday, Tuesday, etc.

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? LastRunDate { get; set; }

        public DateTime? NextRunDate { get; set; }

        public int RunCount { get; set; } = 0;

        [MaxLength(50)]
        public string? Status { get; set; } = "Active"; // Active, Paused, Completed, Error

        [MaxLength(500)]
        public string? LastRunStatus { get; set; } // Success, Failed, Partial

        [MaxLength(1000)]
        public string? LastRunMessage { get; set; } // Error message or success details

        [Column(TypeName = "json")]
        public string? ScheduleSettings { get; set; } // JSON for additional schedule settings

        [Column(TypeName = "json")]
        public string? NotificationSettings { get; set; } // JSON for notification preferences

        // Navigation properties
        public virtual ICollection<BillingScheduleTemplate> BillingScheduleTemplates { get; set; } = new List<BillingScheduleTemplate>();
        public virtual ICollection<BillingScheduleRun> BillingScheduleRuns { get; set; } = new List<BillingScheduleRun>();
    }
}
