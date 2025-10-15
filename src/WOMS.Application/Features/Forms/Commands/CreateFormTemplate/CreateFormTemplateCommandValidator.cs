using FluentValidation;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Commands.CreateFormTemplate
{
    public class CreateFormTemplateCommandValidator : AbstractValidator<CreateFormTemplateCommand>
    {
        public CreateFormTemplateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Form template name is required.")
                .MaximumLength(255).WithMessage("Form template name cannot exceed 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");

            RuleFor(x => x.Status)
                .MaximumLength(20).WithMessage("Status cannot exceed 20 characters.");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required.");

            RuleForEach(x => x.Sections)
                .SetValidator(new CreateFormSectionDtoValidator());
        }
    }

    public class CreateFormSectionDtoValidator : AbstractValidator<CreateFormSectionDto>
    {
        public CreateFormSectionDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Section title is required.")
                .MaximumLength(255).WithMessage("Section title cannot exceed 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Section description cannot exceed 1000 characters.");

            RuleFor(x => x.OrderIndex)
                .GreaterThanOrEqualTo(0).WithMessage("Order index must be greater than or equal to 0.");

            RuleForEach(x => x.Fields)
                .SetValidator(new CreateFormFieldDtoValidator());
        }
    }

    public class CreateFormFieldDtoValidator : AbstractValidator<CreateFormFieldDto>
    {
        public CreateFormFieldDtoValidator()
        {
            RuleFor(x => x.FieldType)
                .NotEmpty().WithMessage("Field type is required.")
                .MaximumLength(20).WithMessage("Field type cannot exceed 20 characters.");

            RuleFor(x => x.Label)
                .NotEmpty().WithMessage("Field label is required.")
                .MaximumLength(255).WithMessage("Field label cannot exceed 255 characters.");

            RuleFor(x => x.Placeholder)
                .MaximumLength(255).WithMessage("Placeholder cannot exceed 255 characters.");

            RuleFor(x => x.HelpText)
                .MaximumLength(1000).WithMessage("Help text cannot exceed 1000 characters.");

            RuleFor(x => x.OrderIndex)
                .GreaterThanOrEqualTo(0).WithMessage("Order index must be greater than or equal to 0.");

            RuleFor(x => x.ValidationRules)
                .MaximumLength(2000).WithMessage("Validation rules cannot exceed 2000 characters.");

            RuleFor(x => x.Options)
                .MaximumLength(2000).WithMessage("Options cannot exceed 2000 characters.");

            RuleFor(x => x.DefaultValue)
                .MaximumLength(1000).WithMessage("Default value cannot exceed 1000 characters.");

            RuleFor(x => x.Pattern)
                .MaximumLength(255).WithMessage("Pattern cannot exceed 255 characters.");

            RuleFor(x => x.MinValue)
                .LessThan(x => x.MaxValue).When(x => x.MinValue.HasValue && x.MaxValue.HasValue)
                .WithMessage("Minimum value must be less than maximum value.");

            RuleFor(x => x.MinLength)
                .LessThan(x => x.MaxLength).When(x => x.MinLength.HasValue && x.MaxLength.HasValue)
                .WithMessage("Minimum length must be less than maximum length.");
        }
    }
}
