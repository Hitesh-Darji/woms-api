using MediatR;

namespace WOMS.Application.Features.Integrations.Commands.SyncIntegration
{
    public class SyncIntegrationCommand : IRequest<SyncIntegrationResult>
    {
        public Guid Id { get; set; }
    }
}


