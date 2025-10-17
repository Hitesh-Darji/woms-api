using MediatR;
using WOMS.Application.Features.RouteOptimization.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.RouteOptimization.Commands.OptimizeAllRoutes
{
    public class OptimizeAllRoutesHandler : IRequestHandler<OptimizeAllRoutesCommand, OptimizeAllRoutesResponse>
    {
        private readonly IRouteRepository _routeRepository;

        public OptimizeAllRoutesHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<OptimizeAllRoutesResponse> Handle(OptimizeAllRoutesCommand request, CancellationToken cancellationToken)
        {
            var routes = await _routeRepository.GetRoutesByDateAsync(request.Date, cancellationToken);
            var routesToOptimize = routes.Where(r => request.ForceReoptimization || r.Status == "Planned").ToList();

            var response = new OptimizeAllRoutesResponse
            {
                RoutesOptimized = routesToOptimize.Count,
                RoutesSkipped = routes.Count() - routesToOptimize.Count,
                Messages = new List<string>()
            };

            // Simulate optimization process
            foreach (var route in routesToOptimize)
            {
                route.Status = "Optimized";
                route.Efficiency = CalculateEfficiency(route);
                route.TotalDistance = CalculateTotalDistance(route);
                route.TotalTime = CalculateTotalTime(route);
                route.TotalStops = route.RouteStops.Count;
                
                response.Messages.Add($"Route for {route.Driver.FirstName} {route.Driver.LastName} optimized successfully");
            }

            // Calculate improvements
            if (routesToOptimize.Any())
            {
                response.AverageEfficiencyImprovement = routesToOptimize.Average(r => r.Efficiency);
                response.TotalDistanceReduction = routesToOptimize.Sum(r => r.TotalDistance);
            }

            return response;
        }

        private static decimal CalculateEfficiency(Domain.Entities.Route route)
        {
            // Simple efficiency calculation based on route stops and distance
            if (route.RouteStops.Count == 0) return 0;
            
            var baseEfficiency = 85m; // Base efficiency
            var stopBonus = Math.Min(route.RouteStops.Count * 2, 15);
            var distancePenalty = Math.Max(route.TotalDistance - 20, 0) * 0.5m; // Penalty for excessive distance
            
            return Math.Min(baseEfficiency + stopBonus - distancePenalty, 100);
        }

        private static decimal CalculateTotalDistance(Domain.Entities.Route route)
        {
            return route.RouteStops.Count * 5.5m; // Average 5.5 km between stops
        }

        private static decimal CalculateTotalTime(Domain.Entities.Route route)
        {
            
            var workTime = route.RouteStops.Sum(rs => rs.EstimatedDuration);
            var travelTime = route.RouteStops.Count * 0.5m; // 30 minutes travel between stops
            return workTime + travelTime;
        }
    }
}
