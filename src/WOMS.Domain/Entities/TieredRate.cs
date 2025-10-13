using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("TieredRate")]
    public class TieredRate : BaseEntity
    {
        [Required]
        public Guid RateTableId { get; set; }

        [ForeignKey(nameof(RateTableId))]
        public virtual RateTable RateTable { get; set; } = null!;

        [Required]
        public int MinQuantity { get; set; }

        public int? MaxQuantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(15,2)")]
        public decimal Rate { get; set; }

        [Required]
        public int OrderIndex { get; set; }
    }
}
