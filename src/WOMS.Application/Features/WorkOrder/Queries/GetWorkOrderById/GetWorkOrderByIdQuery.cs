using MediatR;
using WOMS.Application.Features.WorkOrder.DTOs;

namespace WOMS.Application.Features.WorkOrder.Queries.GetWorkOrderById
{
    public class GetWorkOrderByIdQuery : IRequest<WorkOrderDto?>
    {
        public Guid Id { get; set; }
    }
}
