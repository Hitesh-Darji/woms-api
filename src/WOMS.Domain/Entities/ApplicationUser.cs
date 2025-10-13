using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WOMS.Domain.Entities
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? FullName { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string? City { get; set; }

        [Required]
        [MaxLength(100)]
        public string? PostalCode { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Skills { get; set; } // JSON array of strings

        public UserStatus? Status { get; set; } = UserStatus.Active;

        public DateTime? LastLoginOn { get; set; }


        public Guid? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Department? DepartmentNavigation { get; set; }

        [MaxLength(100)]
        public string? Position { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(450)]
        public string? CreatedBy { get; set; }

        // Additional properties for compatibility
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual ApplicationUser? UpdatedByUser { get; set; }
        public bool IsDeleted { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public virtual ApplicationUser? DeletedByUser { get; set; }

        // Navigation properties
        public virtual ICollection<ApplicationUser> Subordinates { get; set; } = new List<ApplicationUser>();
    }

    public enum UserStatus
    {
        Active,
        Inactive,
        Suspended,
        Pending
    }
}
