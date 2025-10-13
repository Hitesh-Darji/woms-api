using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("BillingTemplate")]
    public class BillingTemplate : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string OutputFormat { get; set; } = "PDF"; // PDF, CSV, Excel, XML, EDI

        [Column(TypeName = "nvarchar(max)")]
        public string? FieldOrder { get; set; } // JSON array as string

        [Column(TypeName = "nvarchar(max)")]
        public string? FieldFormats { get; set; } // JSON as string

        [MaxLength(255)]
        public string? FileNamingConvention { get; set; }

        [Required]
        [MaxLength(20)]
        public string DeliveryMethod { get; set; } = "Email"; // Email, Download, SFTP, API

        [Required]
        [MaxLength(20)]
        public string InvoiceType { get; set; } = "itemized"; // itemized, summary

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<DynamicField> DynamicFields { get; set; } = new List<DynamicField>();
        public virtual ICollection<AggregationRule> AggregationRules { get; set; } = new List<AggregationRule>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    }
}