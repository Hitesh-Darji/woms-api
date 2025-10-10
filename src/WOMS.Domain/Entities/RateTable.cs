using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class RateTable : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string RateTableName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string RateType { get; set; } = "Flat Fee"; // Flat Fee, Hourly, Per Unit, Tiered, etc.

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BaseRate { get; set; } = 0.00m;

        [Required]
        public DateTime EffectiveStartDate { get; set; }

        [Required]
        public DateTime EffectiveEndDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [MaxLength(50)]
        public string? Currency { get; set; } = "USD";

        [MaxLength(100)]
        public string? Category { get; set; } // e.g., "Labor", "Equipment", "Materials", "Service"

        [Column(TypeName = "json")]
        public string? RateRules { get; set; } // JSON for complex pricing rules, tiers, etc.

        [Column(TypeName = "json")]
        public string? AdditionalSettings { get; set; } // JSON for any additional rate settings

        // Navigation properties
        public virtual ICollection<RateTableItem> RateTableItems { get; set; } = new List<RateTableItem>();
    }
}
