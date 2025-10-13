using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("FormAttachment")]
    public class FormAttachment : BaseEntity
    {
        [Required]
        public Guid FormSubmissionId { get; set; }

        [ForeignKey(nameof(FormSubmissionId))]
        public virtual FormSubmission FormSubmission { get; set; } = null!;

        public Guid? FormFieldId { get; set; }

        [ForeignKey(nameof(FormFieldId))]
        public virtual FormField? FormField { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FileType { get; set; }

        public long? FileSize { get; set; }

        [MaxLength(100)]
        public string? MimeType { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(255)]
        public string UploadedBy { get; set; } = string.Empty;
    }
}
