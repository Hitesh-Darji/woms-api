using MediatR;

namespace WOMS.Application.Features.BillingSchedules.Commands.DeleteBillingSchedule
{
    public class DeleteBillingScheduleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}


