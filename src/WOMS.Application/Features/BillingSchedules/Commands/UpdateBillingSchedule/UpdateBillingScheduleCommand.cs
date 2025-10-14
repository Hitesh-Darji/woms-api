using MediatR;
using WOMS.Application.Features.BillingSchedules.DTOs;

namespace WOMS.Application.Features.BillingSchedules.Commands.UpdateBillingSchedule
{
    public class UpdateBillingScheduleCommand : IRequest<BillingScheduleDto>
    {
        public Guid Id { get; set; }
        public UpdateBillingScheduleDto Dto { get; set; } = new UpdateBillingScheduleDto();
    }
}


