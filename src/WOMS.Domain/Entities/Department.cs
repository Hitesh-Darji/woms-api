using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("Department")]
    public class Department : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? Code { get; set; }
        public bool IsActive { get; set; } = true;

        // Override BaseEntity properties to use string for ApplicationUser references
        public new string? CreatedBy { get; set; }
        public new string? UpdatedBy { get; set; }
        public new string? DeletedBy { get; set; }

        // Navigation properties for audit fields
        [ForeignKey(nameof(CreatedBy))]
        public virtual ApplicationUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual ApplicationUser? UpdatedByUser { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public virtual ApplicationUser? DeletedByUser { get; set; }

        // Navigation properties
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
