using FluentValidation;

namespace WOMS.Application.Features.Departments.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Department ID is required.");

            RuleFor(x => x.DeletedBy)
                .NotEmpty().WithMessage("DeletedBy is required.");
        }
    }
}
