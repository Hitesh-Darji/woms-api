using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Invoice")]
    public class Invoice : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; } = string.Empty;

        public Guid? TemplateId { get; set; }

        [ForeignKey(nameof(TemplateId))]
        public virtual BillingTemplate? Template { get; set; }

        [Required]
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        [Required]
        public InvoiceType InvoiceType { get; set; } = InvoiceType.Itemized;

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Tax { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Total { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();
        public virtual ICollection<ApprovalRecord> ApprovalRecords { get; set; } = new List<ApprovalRecord>();
        public virtual ICollection<DeliverySetting> DeliverySettings { get; set; } = new List<DeliverySetting>();
    }
}
