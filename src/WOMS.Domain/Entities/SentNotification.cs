using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("SentNotification")]
    public class SentNotification : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        [Column(TypeName = "json")]
        public string? Metadata { get; set; }

        [Required]
        [MaxLength(200)]
        public string Recipient { get; set; } = string.Empty;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "sent";

        [MaxLength(200)]
        public string? Subject { get; set; }

        [Required]
        public Guid TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual NotificationTemplate Template { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
