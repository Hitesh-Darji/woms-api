using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("FormSignature")]
    public class FormSignature : BaseEntity
    {
        [Required]
        public Guid FormSubmissionId { get; set; }

        [ForeignKey(nameof(FormSubmissionId))]
        public virtual FormSubmission FormSubmission { get; set; } = null!;

        public Guid? FormFieldId { get; set; }

        [ForeignKey(nameof(FormFieldId))]
        public virtual FormField? FormField { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string SignatureData { get; set; } = string.Empty; // Base64 encoded signature

        [Required]
        [MaxLength(20)]
        public string SignatureType { get; set; } = "draw"; // draw, type, upload

        [Required]
        [MaxLength(255)]
        public string SignerName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? SignerEmail { get; set; }

        [Required]
        public DateTime SignedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(45)]
        public string? IpAddress { get; set; }

        [Required]
        public bool IsVerified { get; set; } = false;
    }
}
