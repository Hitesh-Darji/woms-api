using MediatR;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Domain.Enums;

namespace WOMS.Application.Features.Integrations.Queries.GetAllIntegrations
{
    public class GetAllIntegrationsQuery : IRequest<IEnumerable<IntegrationDto>>
    {
        public IntegrationCategory? Category { get; set; }
        public IntegrationStatus? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}

