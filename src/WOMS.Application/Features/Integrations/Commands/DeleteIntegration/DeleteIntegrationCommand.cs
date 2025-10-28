using MediatR;

namespace WOMS.Application.Features.Integrations.Commands.DeleteIntegration
{
    public class DeleteIntegrationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}

