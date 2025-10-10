using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("WorkOrderAttachment")]
    public class WorkOrderAttachment : BaseEntity
    {
        [Required]
        public Guid WorkOrderId { get; set; }

        [ForeignKey(nameof(WorkOrderId))]
        public virtual WorkOrder WorkOrder { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public long? FileSize { get; set; }

        [MaxLength(100)]
        public string? FileType { get; set; }

        public Guid? UploadedBy { get; set; }

        [ForeignKey(nameof(UploadedBy))]
        public virtual ApplicationUser? UploadedByUser { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
