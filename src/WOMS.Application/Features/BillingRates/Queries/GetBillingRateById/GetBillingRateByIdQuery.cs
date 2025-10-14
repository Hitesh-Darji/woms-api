using MediatR;
using WOMS.Application.Features.BillingRates.DTOs;

namespace WOMS.Application.Features.BillingRates.Queries.GetBillingRateById
{
    public class GetBillingRateByIdQuery : IRequest<BillingRateDto?>
    {
        public Guid Id { get; set; }
    }
}
