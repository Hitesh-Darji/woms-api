using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class Vendor : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [MaxLength(100)]
        public string? ContactPerson { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(50)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [Required]
        public VendorStatus Status { get; set; } = VendorStatus.Active;

        [MaxLength(100)]
        public string? Website { get; set; }

        [MaxLength(100)]
        public string? TaxId { get; set; }

        [Column(TypeName = "json")]
        public string? Terms { get; set; } // JSON for payment terms, delivery terms, etc.

        // Navigation properties
        public virtual ICollection<InventoryDisposition> InventoryDispositions { get; set; } = new List<InventoryDisposition>();
    }
}
