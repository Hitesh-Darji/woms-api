using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("FormSubmission")]
    public class FormSubmission : BaseEntity
    {
        [Required]
        public Guid FormTemplateId { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        public virtual FormTemplate FormTemplate { get; set; } = null!;

        public Guid? WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder? WorkOrder { get; set; }

        [Required]
        [MaxLength(255)]
        public string SubmittedBy { get; set; } = string.Empty;

        [Required]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public FormSubmissionStatus Status { get; set; } = FormSubmissionStatus.Draft;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Data { get; set; } = string.Empty; // JSON as string

        [Column(TypeName = "nvarchar(max)")]
        public string? LocationData { get; set; } // JSON as string

        [Column(TypeName = "nvarchar(max)")]
        public string? DeviceInfo { get; set; } // JSON as string

        [MaxLength(45)]
        public string? IpAddress { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? UserAgent { get; set; }

        [Required]
        public bool IsOffline { get; set; } = false;

        [Required]
        public SyncStatus SyncStatus { get; set; } = SyncStatus.Pending;

        public DateTime? LastSyncAt { get; set; }

        // Navigation properties
        public virtual ICollection<FormAttachment> FormAttachments { get; set; } = new List<FormAttachment>();
        public virtual ICollection<FormSignature> FormSignatures { get; set; } = new List<FormSignature>();
        public virtual ICollection<FormGeolocation> FormGeolocations { get; set; } = new List<FormGeolocation>();
    }
}
