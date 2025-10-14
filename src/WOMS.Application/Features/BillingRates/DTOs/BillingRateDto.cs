using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Application.Features.BillingRates.DTOs
{
    public class BillingRateDto
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string RateType { get; set; } = "Flat Fee";
        
        [Column(TypeName = "decimal(15,2)")]
        public decimal? BaseRate { get; set; }
        
        [Required]
        public DateTime EffectiveStartDate { get; set; }
        
        [Required]
        public DateTime EffectiveEndDate { get; set; }
        
        [Required]
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
