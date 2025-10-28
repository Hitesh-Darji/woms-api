using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Integrations.Queries.GetAllIntegrations
{
    public class GetAllIntegrationsQueryHandler : IRequestHandler<GetAllIntegrationsQuery, IEnumerable<IntegrationDto>>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IMapper _mapper;

        public GetAllIntegrationsQueryHandler(
            IIntegrationRepository integrationRepository, IMapper mapper)
        {
            _integrationRepository = integrationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IntegrationDto>> Handle(GetAllIntegrationsQuery request, CancellationToken cancellationToken)
        {
            var query = _integrationRepository.GetQueryable()
                .Where(i => !i.IsDeleted);

            // Apply filters
            if (request.Category.HasValue)
            {
                query = query.Where(i => i.Category == request.Category.Value);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(i => i.Status == request.Status.Value);
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(i => i.IsActive == request.IsActive.Value);
            }

            var integrations = await query
                .OrderBy(i => i.Category)
                .ThenBy(i => i.Name)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<IntegrationDto>>(integrations);
        }
    }
}

