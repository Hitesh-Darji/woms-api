using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class TechnicianZone : BaseEntity
    {
        [Required]
        public Guid TechnicianId { get; set; }

        [ForeignKey(nameof(TechnicianId))]
        public virtual ApplicationUser Technician { get; set; } = null!;

        [Required]
        public Guid ZoneId { get; set; }

        [ForeignKey(nameof(ZoneId))]
        public virtual Zone Zone { get; set; } = null!;

        [MaxLength(50)]
        public string Status { get; set; } = "Active"; // Active, Inactive

        public bool IsPrimary { get; set; } = false; // Primary zone for the technician

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
