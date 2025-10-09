using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
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

        [Column(TypeName = "json")]
        public string? Skills { get; set; } // JSON array of strings

        public UserStatus? Status { get; set; } = UserStatus.Active;

        public DateTime? LastLoginOn { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual ApplicationUser? UpdatedByUser { get; set; }

        [ForeignKey(nameof(DeletedBy))]
        public virtual ApplicationUser? DeletedByUser { get; set; }

        // Navigation properties for technician functionality
        public virtual ICollection<TechnicianSkill> TechnicianSkills { get; set; } = new List<TechnicianSkill>();
        public virtual ICollection<TechnicianEquipment> TechnicianEquipments { get; set; } = new List<TechnicianEquipment>();
        public virtual ICollection<TechnicianZone> TechnicianZones { get; set; } = new List<TechnicianZone>();
        public virtual ICollection<AssignmentTemplateTechnician> AssignmentTemplateTechnicians { get; set; } = new List<AssignmentTemplateTechnician>();
        public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
    }
}
