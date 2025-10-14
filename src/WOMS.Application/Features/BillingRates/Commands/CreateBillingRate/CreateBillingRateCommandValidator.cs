using FluentValidation;

namespace WOMS.Application.Features.BillingRates.Commands.CreateBillingRate
{
    public class CreateBillingRateCommandValidator : AbstractValidator<CreateBillingRateCommand>
    {
        public CreateBillingRateCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty().WithMessage("Rate table name is required.")
                .MaximumLength(255).WithMessage("Rate table name cannot exceed 255 characters.");

            RuleFor(x => x.Dto.RateType)
                .NotEmpty().WithMessage("Rate type is required.")
                .MaximumLength(20).WithMessage("Rate type cannot exceed 20 characters.");

            RuleFor(x => x.Dto.EffectiveStartDate)
                .NotEmpty().WithMessage("Effective start date is required.");

            RuleFor(x => x.Dto.EffectiveEndDate)
                .NotEmpty().WithMessage("Effective end date is required.")
                .GreaterThan(x => x.Dto.EffectiveStartDate).WithMessage("Effective end date must be after effective start date.");

            RuleFor(x => x.Dto.BaseRate)
                .GreaterThanOrEqualTo(0).WithMessage("Base rate must be greater than or equal to 0.")
                .When(x => x.Dto.BaseRate.HasValue);
        }
    }
}
