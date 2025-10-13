using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("RateTable")]
    public class RateTable : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string RateType { get; set; } = "flat"; // flat, hourly, tiered, unit, conditional

        [Column(TypeName = "decimal(15,2)")]
        public decimal? BaseRate { get; set; }

        [Required]
        public DateTime EffectiveStartDate { get; set; }

        [Required]
        public DateTime EffectiveEndDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<TieredRate> TieredRates { get; set; } = new List<TieredRate>();
        public virtual ICollection<ConditionalRate> ConditionalRates { get; set; } = new List<ConditionalRate>();
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; } = new List<InvoiceLineItem>();
    }
}