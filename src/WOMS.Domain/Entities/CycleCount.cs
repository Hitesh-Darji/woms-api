using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    [Table("CycleCount")]
    public class CycleCount : BaseEntity
    {
        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public CycleCountType Type { get; set; } = CycleCountType.FullCount; // full, partial

        [Required]
        [MaxLength(20)]
        public CycleCountStatus Status { get; set; } = CycleCountStatus.Planned; // planned, in-progress, completed, cancelled

        [Required]
        public DateTime PlannedDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [Required]
        [MaxLength(255)]
        public string CountedBy { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? SupervisorApproval { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        // Navigation properties
        public virtual ICollection<CountItem> CountItems { get; set; } = new List<CountItem>();
    }
}