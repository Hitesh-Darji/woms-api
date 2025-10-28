using MediatR;

namespace WOMS.Application.Features.Integrations.Commands.ConnectIntegration
{
    public class ConnectIntegrationCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Configuration { get; set; } = string.Empty;
    }
}

