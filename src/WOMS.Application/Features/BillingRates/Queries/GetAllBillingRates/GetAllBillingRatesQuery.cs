using MediatR;
using WOMS.Application.Features.BillingRates.DTOs;

namespace WOMS.Application.Features.BillingRates.Queries.GetAllBillingRates
{
    public class GetAllBillingRatesQuery : IRequest<IEnumerable<BillingRateDto>>
    {
        public bool? IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
