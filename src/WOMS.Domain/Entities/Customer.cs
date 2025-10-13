using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("Customer")]
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CustomerType Type { get; set; } = CustomerType.Business;

        [Required]
        public CustomerStatus Status { get; set; } = CustomerStatus.Active;

        [MaxLength(50)]
        public string? TaxId { get; set; }

        [MaxLength(100)]
        public string? Industry { get; set; }

        public CustomerSize? Size { get; set; }

        public Guid? PrimaryContactId { get; set; }

        public Guid? AccountManagerId { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal? CreditLimit { get; set; }

        [MaxLength(100)]
        public string? PaymentTerms { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Tags { get; set; } // JSON array as string

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public virtual ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<BillingTemplate> BillingTemplates { get; set; } = new List<BillingTemplate>();
    }
}
