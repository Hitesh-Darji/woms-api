using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("ConditionalRate")]
    public class ConditionalRate : BaseEntity
    {
        [Required]
        public Guid RateTableId { get; set; }

        [ForeignKey(nameof(RateTableId))]
        public virtual RateTable RateTable { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Condition { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string ConditionValue { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Rate { get; set; }

        [Required]
        public int OrderIndex { get; set; }
    }
}
