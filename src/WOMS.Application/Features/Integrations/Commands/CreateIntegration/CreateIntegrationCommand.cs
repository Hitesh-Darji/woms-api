using MediatR;
using WOMS.Application.Features.Integrations.DTOs;

namespace WOMS.Application.Features.Integrations.Commands.CreateIntegration
{
    public class CreateIntegrationCommand : IRequest<IntegrationDto>
    {
        public CreateIntegrationDto Dto { get; set; } = new();
    }
}

