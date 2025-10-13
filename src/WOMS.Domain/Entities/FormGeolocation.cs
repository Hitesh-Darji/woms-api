using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("FormGeolocation")]
    public class FormGeolocation : BaseEntity
    {
        [Required]
        public Guid FormSubmissionId { get; set; }

        [ForeignKey(nameof(FormSubmissionId))]
        public virtual FormSubmission FormSubmission { get; set; } = null!;

        public Guid? FormFieldId { get; set; }

        [ForeignKey(nameof(FormFieldId))]
        public virtual FormField? FormField { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 8)")]
        public decimal Latitude { get; set; }

        [Required]
        [Column(TypeName = "decimal(11, 8)")]
        public decimal Longitude { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Altitude { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Accuracy { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Address { get; set; }

        [Required]
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsManual { get; set; } = false; // Manual entry vs GPS
    }
}
