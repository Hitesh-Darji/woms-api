using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Address")]
    public class Address : BaseEntity
    {
        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Type { get; set; } = "billing"; // billing, shipping, service, mailing

        [Required]
        [MaxLength(255)]
        public string Street1 { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Street2 { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        public bool IsPrimary { get; set; } = false;

        [Column(TypeName = "nvarchar(max)")]
        public string? Coordinates { get; set; } // JSON as string
    }
}
