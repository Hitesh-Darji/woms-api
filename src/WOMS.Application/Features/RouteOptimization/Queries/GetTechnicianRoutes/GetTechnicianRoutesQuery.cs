using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;

namespace WOMS.Application.Features.RouteOptimization.Queries.GetTechnicianRoutes
{
    public class GetTechnicianRoutesQuery : IRequest<TechnicianRoutesResponse>
    {
        public DateTime Date { get; set; }
        public string? TechnicianId { get; set; }
    }
}
