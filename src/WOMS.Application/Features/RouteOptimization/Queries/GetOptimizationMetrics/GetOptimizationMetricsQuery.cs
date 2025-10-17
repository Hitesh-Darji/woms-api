using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;

namespace WOMS.Application.Features.RouteOptimization.Queries.GetOptimizationMetrics
{
    public class GetOptimizationMetricsQuery : IRequest<RouteOptimizationMetricsDto>
    {
        public DateTime Date { get; set; }
    }
}
