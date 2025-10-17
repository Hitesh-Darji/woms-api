using MediatR;

namespace WOMS.Application.Features.WorkOrder.Commands.DeleteWorkOrder
{
    public class DeleteWorkOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

