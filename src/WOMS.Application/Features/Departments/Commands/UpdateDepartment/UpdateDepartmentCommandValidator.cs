using FluentValidation;

namespace WOMS.Application.Features.Departments.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Department ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required.")
                .MaximumLength(100).WithMessage("Department name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Code)
                .MaximumLength(50).WithMessage("Code cannot exceed 50 characters.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(20).WithMessage("Status cannot exceed 20 characters.");

            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("UpdatedBy is required.");
        }
    }
}
