using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("OptimizationSettings")]
    public class OptimizationSettings : BaseEntity
    {
        [Required]
        public bool PrioritizeDistanceReduction { get; set; } = true;

        [Required]
        public bool RespectTimeWindows { get; set; } = true;

        [Required]
        public bool BalanceWorkload { get; set; } = false;

        [Required]
        public bool ConsiderTrafficPatterns { get; set; } = true;

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal MaxRouteTimeHours { get; set; } = 8;

        [Required]
        [MaxLength(450)]
        public new string CreatedBy { get; set; } = string.Empty;

        [MaxLength(450)]
        public new string? UpdatedBy { get; set; }
    }
}
