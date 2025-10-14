using WOMS.Domain.Entities;

namespace WOMS.Application.Features.BillingTemplates.DTOs
{
    public class BillingTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string OutputFormat { get; set; } = string.Empty;
        public string? FileNamingConvention { get; set; }
        public string DeliveryMethod { get; set; } = string.Empty;
        public string InvoiceType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        
        // Field order configuration
        public List<BillingTemplateFieldDto> FieldOrder { get; set; } = new List<BillingTemplateFieldDto>();
    }

    public class BillingTemplateFieldDto
    {
        public string FieldName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsEnabled { get; set; }
        public string? DisplayLabel { get; set; }
        public string? FieldType { get; set; }
        public string? FieldSettings { get; set; }
    }
}
