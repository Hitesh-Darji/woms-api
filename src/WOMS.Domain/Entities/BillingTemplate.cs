using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class BillingTemplate : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string TemplateName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string OutputFormat { get; set; } = "PDF"; // PDF, Excel, CSV, etc.

        [Required]
        [MaxLength(20)]
        public string DeliveryMethod { get; set; } = "Email"; // Email, FTP, API, etc.

        [Required]
        [MaxLength(50)]
        public string InvoiceType { get; set; } = "Itemized"; // Itemized, Summary, Detailed, etc.

        [Required]
        [MaxLength(200)]
        public string FileNamingConvention { get; set; } = "INV-{customer}-{date}-{number}";

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        [Column(TypeName = "json")]
        public string? AdditionalSettings { get; set; } // JSON for any additional template settings

        // Navigation properties
        public virtual ICollection<BillingTemplateFieldOrder> FieldOrders { get; set; } = new List<BillingTemplateFieldOrder>();
    }
}
