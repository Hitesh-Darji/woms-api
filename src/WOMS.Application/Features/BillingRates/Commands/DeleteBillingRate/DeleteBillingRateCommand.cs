using MediatR;

namespace WOMS.Application.Features.BillingRates.Commands.DeleteBillingRate
{
    public class DeleteBillingRateCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
