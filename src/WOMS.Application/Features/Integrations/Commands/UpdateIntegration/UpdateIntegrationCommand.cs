using MediatR;
using WOMS.Application.Features.Integrations.DTOs;

namespace WOMS.Application.Features.Integrations.Commands.UpdateIntegration
{
    public class UpdateIntegrationCommand : IRequest<IntegrationDto>
    {
        public Guid Id { get; set; }
        public UpdateIntegrationDto Dto { get; set; } = new();
    }
}

