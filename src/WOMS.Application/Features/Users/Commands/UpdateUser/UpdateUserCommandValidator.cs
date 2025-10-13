using FluentValidation;

namespace WOMS.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UpdateUserDto).NotNull().WithMessage("Update user data is required.");

            RuleFor(x => x.UpdateUserDto.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200).WithMessage("Full name cannot exceed 200 characters.");

            RuleFor(x => x.UpdateUserDto.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(x => x.UpdateUserDto.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.UpdateUserDto.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.")
                .MaximumLength(100).WithMessage("Postal code cannot exceed 100 characters.");

            RuleFor(x => x.UpdateUserDto.Phone)
                .MaximumLength(20).WithMessage("Phone cannot exceed 20 characters.");

            RuleFor(x => x.UpdateUserDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

            RuleFor(x => x.UpdateUserDto.RoleId)
                .NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
