using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class BillingScheduleRun : BaseEntity
    {
        [Required]
        public Guid BillingScheduleId { get; set; }

        [ForeignKey(nameof(BillingScheduleId))]
        public virtual BillingSchedule BillingSchedule { get; set; } = null!;

        [Required]
        public DateTime RunDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ScheduledRunDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Running"; // Running, Completed, Failed, Cancelled

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public int? DurationSeconds { get; set; } // Duration in seconds

        public int TemplatesProcessed { get; set; } = 0;

        public int TemplatesSuccessful { get; set; } = 0;

        public int TemplatesFailed { get; set; } = 0;

        public int InvoicesGenerated { get; set; } = 0;

        public int EmailsSent { get; set; } = 0;

        [MaxLength(1000)]
        public string? ErrorMessage { get; set; }

        [MaxLength(2000)]
        public string? Summary { get; set; } // Summary of the run results

        [Column(TypeName = "nvarchar(max)")]
        public string? RunDetails { get; set; } // JSON for detailed run information

        [Column(TypeName = "nvarchar(max)")]
        public string? ErrorDetails { get; set; } // JSON for detailed error information
    }
}
