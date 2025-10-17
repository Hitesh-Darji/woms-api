using MediatR;
using WOMS.Application.Features.Assignment.DTOs;

namespace WOMS.Application.Features.Assignment.Queries.GetAssignmentRecommendations
{
    public class GetAssignmentRecommendationsQuery : IRequest<AssignmentRecommendationsDto?>
    {
        public Guid WorkOrderId { get; set; }
    }
}
