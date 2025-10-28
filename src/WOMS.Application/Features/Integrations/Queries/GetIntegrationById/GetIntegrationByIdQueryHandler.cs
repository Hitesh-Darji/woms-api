using AutoMapper;
using MediatR;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Integrations.Queries.GetIntegrationById
{
    public class GetIntegrationByIdQueryHandler : IRequestHandler<GetIntegrationByIdQuery, IntegrationDto?>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetIntegrationByIdQueryHandler(
            IIntegrationRepository integrationRepository,
            AutoMapper.IMapper mapper)
        {
            _integrationRepository = integrationRepository;
            _mapper = mapper;
        }

        public async Task<IntegrationDto?> Handle(GetIntegrationByIdQuery request, CancellationToken cancellationToken)
        {
            var integration = await _integrationRepository.GetByIdAsync(request.Id, cancellationToken);
            
            if (integration == null || integration.IsDeleted)
            {
                return null;
            }

            return _mapper.Map<IntegrationDto>(integration);
        }
    }
}

