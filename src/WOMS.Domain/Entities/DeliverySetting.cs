using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("DeliverySetting")]
    public class DeliverySetting : BaseEntity
    {
        [Required]
        public Guid InvoiceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public virtual Invoice Invoice { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Method { get; set; } = "Email"; // Email, Download, SFTP, API

        [Column(TypeName = "nvarchar(max)")]
        public string? EmailRecipients { get; set; } // JSON array as string

        [Column(TypeName = "nvarchar(max)")]
        public string? SftpSettings { get; set; } // JSON as string

        [Column(TypeName = "nvarchar(max)")]
        public string? ApiSettings { get; set; } // JSON as string
    }
}
