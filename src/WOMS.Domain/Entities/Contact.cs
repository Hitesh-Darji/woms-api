using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Contact")]
    public class Contact : BaseEntity
    {
        [Required]
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public string? Department { get; set; }

        [MaxLength(255)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(20)]
        public string? Mobile { get; set; }

        [MaxLength(20)]
        public string? Fax { get; set; }

        [Required]
        public bool IsPrimary { get; set; } = false;

        [Required]
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

    }
}
