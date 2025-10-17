using AutoMapper;
using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;
using WOMS.Application.Features.RouteOptimization.Queries.GetOptimizationMetrics;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.RouteOptimization.Queries.GetOptimizationMetrics
{
    public class GetOptimizationMetricsHandler : IRequestHandler<GetOptimizationMetricsQuery, RouteOptimizationMetricsDto>
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public GetOptimizationMetricsHandler(IRouteRepository routeRepository, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        public async Task<RouteOptimizationMetricsDto> Handle(GetOptimizationMetricsQuery request, CancellationToken cancellationToken)
        {
            var routes = await _routeRepository.GetRoutesByDateAsync(request.Date, cancellationToken);
            
            var metrics = new RouteOptimizationMetricsDto
            {
                AverageEfficiency = await _routeRepository.GetAverageEfficiencyAsync(request.Date, cancellationToken),
                TotalDistance = await _routeRepository.GetTotalDistanceAsync(request.Date, cancellationToken),
                TotalTime = await _routeRepository.GetTotalTimeAsync(request.Date, cancellationToken),
                TotalStops = await _routeRepository.GetTotalStopsAsync(request.Date, cancellationToken),
                TotalRoutes = routes.Count(),
                OptimizedRoutes = routes.Count(r => r.Status == "Optimized"),
                PendingRoutes = routes.Count(r => r.Status == "Planned")
            };

            return metrics;
        }
    }
}
