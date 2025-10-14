using System.ComponentModel.DataAnnotations;

namespace WOMS.Application.Features.BillingTemplates.DTOs
{
    public class CreateBillingTemplateDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string OutputFormat { get; set; } = "PDF";

        [MaxLength(255)]
        public string? FileNamingConvention { get; set; }

        [Required]
        [MaxLength(20)]
        public string DeliveryMethod { get; set; } = "Email";

        [Required]
        [MaxLength(20)]
        public string InvoiceType { get; set; } = "Itemized (Per Job Line)";

        [Required]
        public List<BillingTemplateFieldDto> FieldOrder { get; set; } = new List<BillingTemplateFieldDto>();
    }
}
