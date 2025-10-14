using FluentValidation;

namespace WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate
{
    public class UpdateBillingTemplateCommandValidator : AbstractValidator<UpdateBillingTemplateCommand>
    {
        public UpdateBillingTemplateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Template ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Template name is required.")
                .MaximumLength(255).WithMessage("Template name cannot exceed 255 characters.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.")
                .MaximumLength(255).WithMessage("Customer ID cannot exceed 255 characters.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(255).WithMessage("Customer name cannot exceed 255 characters.");

            RuleFor(x => x.OutputFormat)
                .NotEmpty().WithMessage("Output format is required.")
                .Must(format => new[] { "PDF", "CSV", "Excel", "XML", "EDI" }.Contains(format))
                .WithMessage("Output format must be one of: PDF, CSV, Excel, XML, EDI.");

            RuleFor(x => x.DeliveryMethod)
                .NotEmpty().WithMessage("Delivery method is required.")
                .Must(method => new[] { "Email", "Download", "SFTP", "API" }.Contains(method))
                .WithMessage("Delivery method must be one of: Email, Download, SFTP, API.");

            RuleFor(x => x.InvoiceType)
                .NotEmpty().WithMessage("Invoice type is required.")
                .Must(type => new[] { 
                    "Itemized (Per Job Line)", 
                    "Summary", 
                    "Service", 
                    "Product", 
                    "Time & Materials",
                    "Fixed Price",
                    "itemized", 
                    "summary",
                    "service",
                    "product",
                    "time_materials",
                    "fixed_price"
                }.Contains(type))
                .WithMessage("Invoice type must be one of: Itemized (Per Job Line), Summary, Service, Product, Time & Materials, Fixed Price.");

            RuleFor(x => x.FileNamingConvention)
                .MaximumLength(255).WithMessage("File naming convention cannot exceed 255 characters.")
                .When(x => !string.IsNullOrEmpty(x.FileNamingConvention));

            RuleFor(x => x.FieldOrder)
                .NotNull().WithMessage("Field order is required.")
                .Must(fields => fields != null && fields.Any()).WithMessage("At least one field must be specified.");

            RuleForEach(x => x.FieldOrder)
                .ChildRules(field =>
                {
                    field.RuleFor(f => f.FieldName)
                        .NotEmpty().WithMessage("Field name is required.")
                        .MaximumLength(100).WithMessage("Field name cannot exceed 100 characters.");
                    
                    field.RuleFor(f => f.DisplayOrder)
                        .GreaterThanOrEqualTo(0).WithMessage("Display order must be non-negative.");
                });
        }
    }
}
