using MediatR;
using WOMS.Application.Features.Assignment.DTOs;

namespace WOMS.Application.Features.Assignment.Queries.GetUnassignedWorkOrders
{
    public class GetUnassignedWorkOrdersQuery : IRequest<UnassignedWorkOrdersResponse>
    {
        public string? Priority { get; set; }
        public string? WorkType { get; set; }
        public string? Location { get; set; }
    }
}
