using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WOMS.Domain.Enums;

namespace WOMS.Domain.Entities
{
    public class CycleCount : BaseEntity
    {
        [Required]
        public Guid LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; } = null!;

        [Required]
        public CycleCountType CountType { get; set; } = CycleCountType.FullCount;

        [Required]
        public DateTime PlannedDate { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [Required]
        public CycleCountStatus Status { get; set; } = CycleCountStatus.Planned;

        [MaxLength(1000)]
        public string? Notes { get; set; }

        [Required]
        public Guid CountedByUserId { get; set; }

        [ForeignKey(nameof(CountedByUserId))]
        public virtual ApplicationUser CountedByUser { get; set; } = null!;

        public int TotalItems { get; set; } = 0;

        public int CountedItems { get; set; } = 0;

        public int VarianceItems { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<CycleCountItem> CycleCountItems { get; set; } = new List<CycleCountItem>();
    }
}
