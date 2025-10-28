using MediatR;
using WOMS.Application.Features.Integrations.DTOs;

namespace WOMS.Application.Features.Integrations.Queries.GetIntegrationById
{
    public class GetIntegrationByIdQuery : IRequest<IntegrationDto?>
    {
        public Guid Id { get; set; }
    }
}

