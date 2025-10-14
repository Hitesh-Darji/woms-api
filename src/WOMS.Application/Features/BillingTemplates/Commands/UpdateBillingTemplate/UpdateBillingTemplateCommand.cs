using MediatR;
using WOMS.Application.Features.BillingTemplates.DTOs;

namespace WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate
{
    public record UpdateBillingTemplateCommand : IRequest<BillingTemplateDto>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string CustomerId { get; init; } = string.Empty;
        public string CustomerName { get; init; } = string.Empty;
        public string OutputFormat { get; init; } = "PDF";
        public string? FileNamingConvention { get; init; }
        public string DeliveryMethod { get; init; } = "Email";
        public string InvoiceType { get; init; } = "Itemized (Per Job Line)";
        public List<BillingTemplateFieldDto> FieldOrder { get; init; } = new List<BillingTemplateFieldDto>();
        public bool IsActive { get; init; } = true;
    }
}
