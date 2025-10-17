using MediatR;

namespace WOMS.Application.Features.Assignment.Commands.AssignWorkOrder
{
    public class AssignWorkOrderCommand : IRequest<bool>
    {
        public Guid WorkOrderId { get; set; }
        public string TechnicianId { get; set; } = string.Empty;
    }
}
