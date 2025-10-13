using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("AspNetRoles")]
    public class ApplicationRole : IdentityRole
    {
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Required]
        public bool IsClient { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [MaxLength(450)]
        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? DeletedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Additional properties for compatibility
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
