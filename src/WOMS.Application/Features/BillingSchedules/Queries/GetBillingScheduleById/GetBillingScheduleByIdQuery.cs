using MediatR;
using WOMS.Application.Features.BillingSchedules.DTOs;

namespace WOMS.Application.Features.BillingSchedules.Queries.GetBillingScheduleById
{
    public class GetBillingScheduleByIdQuery : IRequest<BillingScheduleDto>
    {
        public Guid Id { get; set; }
    }
}


