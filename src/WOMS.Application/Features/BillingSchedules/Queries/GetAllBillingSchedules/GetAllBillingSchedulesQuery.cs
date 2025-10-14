using MediatR;
using WOMS.Application.Features.BillingSchedules.DTOs;

namespace WOMS.Application.Features.BillingSchedules.Queries.GetAllBillingSchedules
{
    public class GetAllBillingSchedulesQuery : IRequest<IEnumerable<BillingScheduleDto>>
    {
        public bool? IsActive { get; set; }
    }
}


