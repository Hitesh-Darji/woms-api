using MediatR;
using WOMS.Application.Features.BillingSchedules.DTOs;

namespace WOMS.Application.Features.BillingSchedules.Commands.CreateBillingSchedule
{
    public class CreateBillingScheduleCommand : IRequest<BillingScheduleDto>
    {
        public CreateBillingScheduleDto Dto { get; set; } = new CreateBillingScheduleDto();
    }
}


