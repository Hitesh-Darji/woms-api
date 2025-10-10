using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class RateTableItem : BaseEntity
    {
        [Required]
        public Guid RateTableId { get; set; }

        [ForeignKey(nameof(RateTableId))]
        public virtual RateTable RateTable { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rate { get; set; }

        [MaxLength(50)]
        public string? Unit { get; set; } // e.g., "hour", "unit", "sqft", "each"

        [MaxLength(100)]
        public string? Category { get; set; } // e.g., "Labor", "Equipment", "Materials"

        [MaxLength(50)]
        public string? SkillLevel { get; set; } // e.g., "Basic", "Intermediate", "Advanced", "Expert"

        [MaxLength(100)]
        public string? WorkType { get; set; } // e.g., "Installation", "Repair", "Maintenance"

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        [Column(TypeName = "json")]
        public string? Conditions { get; set; } // JSON for rate conditions (minimum hours, location-based, etc.)

        [Column(TypeName = "json")]
        public string? AdditionalSettings { get; set; } // JSON for item-specific settings
    }
}
