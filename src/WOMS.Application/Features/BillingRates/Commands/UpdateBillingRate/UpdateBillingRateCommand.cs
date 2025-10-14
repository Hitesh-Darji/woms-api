using MediatR;
using WOMS.Application.Features.BillingRates.DTOs;

namespace WOMS.Application.Features.BillingRates.Commands.UpdateBillingRate
{
    public class UpdateBillingRateCommand : IRequest<BillingRateDto>
    {
        public Guid Id { get; set; }
        public UpdateBillingRateDto Dto { get; set; } = new();
    }
}
