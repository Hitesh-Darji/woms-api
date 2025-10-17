using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;

namespace WOMS.Application.Features.RouteOptimization.Commands.OptimizeAllRoutes
{
    public class OptimizeAllRoutesCommand : IRequest<OptimizeAllRoutesResponse>
    {
        public DateTime Date { get; set; }
        public bool ForceReoptimization { get; set; }
    }
}
