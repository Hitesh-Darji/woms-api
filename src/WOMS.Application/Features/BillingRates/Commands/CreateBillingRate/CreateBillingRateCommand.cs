using MediatR;
using WOMS.Application.Features.BillingRates.DTOs;

namespace WOMS.Application.Features.BillingRates.Commands.CreateBillingRate
{
    public class CreateBillingRateCommand : IRequest<BillingRateDto>
    {
        public CreateBillingRateDto Dto { get; set; } = new();
    }
}
