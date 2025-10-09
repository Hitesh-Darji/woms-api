using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class JobKit : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(50)]
        public string? JobType { get; set; } // Meter Install, Transformer Replace, Emergency Repair

        public bool IsActive { get; set; } = true;

        [Required]
        public JobKitStatus Status { get; set; } = JobKitStatus.Active;

        [Required]
        public JobKitAvailability Availability { get; set; } = JobKitAvailability.Available;

        public int TotalItems { get; set; } = 0;

        public int OptionalItems { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<JobKitItem> JobKitItems { get; set; } = new List<JobKitItem>();
    }
}
