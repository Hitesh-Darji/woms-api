using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("NotificationTemplate")]
    public class NotificationTemplate : BaseEntity
    {
        public bool Active { get; set; } = true;

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "json")]
        public string? Placeholders { get; set; } // JSON array of strings

        [MaxLength(200)]
        public string? Subject { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<SentNotification> SentNotifications { get; set; } = new List<SentNotification>();
    }
}
