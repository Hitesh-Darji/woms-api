using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("ValidationIssue")]
    public class ValidationIssue : BaseEntity
    {
        [Required]
        public Guid AssetId { get; set; }

        [ForeignKey(nameof(AssetId))]
        public virtual Asset Asset { get; set; } = null!;

        [Required]
        public ValidationIssueType Type { get; set; } = ValidationIssueType.Error;

        [Required]
        [MaxLength(100)]
        public string Code { get; set; } = string.Empty; // MISSING_SERIAL_NUMBER, INVALID_MODEL_FORMAT, etc.

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        public bool IsResolved { get; set; } = false;

        public string? ResolvedByUserId { get; set; }

        [ForeignKey(nameof(ResolvedByUserId))]
        public virtual ApplicationUser? ResolvedByUser { get; set; }

        public DateTime? ResolvedOn { get; set; }

        [Required]
        public DateTime OccurredOn { get; set; } = DateTime.UtcNow;

        [MaxLength(1000)]
        public string? ResolutionNotes { get; set; }

        [Required]
        public ValidationIssueSeverity Severity { get; set; } = ValidationIssueSeverity.Medium;

        [Column(TypeName = "nvarchar(max)")]
        public string? Metadata { get; set; } // JSON for additional issue-specific data
    }
}
