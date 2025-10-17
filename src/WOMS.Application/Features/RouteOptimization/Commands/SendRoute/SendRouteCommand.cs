using MediatR;

namespace WOMS.Application.Features.RouteOptimization.Commands.SendRoute
{
    public class SendRouteCommand : IRequest<bool>
    {
        public Guid RouteId { get; set; }
    }
}
