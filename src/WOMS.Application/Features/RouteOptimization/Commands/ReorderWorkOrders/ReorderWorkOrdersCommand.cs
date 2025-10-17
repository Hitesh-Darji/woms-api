using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;

namespace WOMS.Application.Features.RouteOptimization.Commands.ReorderWorkOrders
{
    public class ReorderWorkOrdersCommand : IRequest<ReorderWorkOrdersResponse>
    {
        public Guid RouteId { get; set; }
        public Guid WorkOrderId { get; set; }
        public ReorderDirection Direction { get; set; }
    }
}