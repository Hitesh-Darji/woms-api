using MediatR;
using WOMS.Application.Features.RouteOptimization.Commands.SendRoute;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.RouteOptimization.Commands.SendRoute
{
    public class SendRouteHandler : IRequestHandler<SendRouteCommand, bool>
    {
        private readonly IRouteRepository _routeRepository;

        public SendRouteHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<bool> Handle(SendRouteCommand request, CancellationToken cancellationToken)
        {
            var route = await _routeRepository.GetRouteWithStopsAsync(request.RouteId, cancellationToken);
            
            if (route == null)
                return false;

            // Update route status and dispatch time
            route.Status = "Dispatched";
            route.DispatchedAt = DateTime.UtcNow;
            route.UpdatedOn = DateTime.UtcNow;

            return true;
        }
    }
}
