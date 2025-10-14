using MediatR;
using System.Text.Json;
using WOMS.Application.Features.BillingSchedules.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingSchedules.Queries.GetAllBillingSchedules
{
    public class GetAllBillingSchedulesQueryHandler : IRequestHandler<GetAllBillingSchedulesQuery, IEnumerable<BillingScheduleDto>>
    {
        private readonly IBillingScheduleRepository _repository;
        private readonly Interfaces.IMapper _mapper;

        public GetAllBillingSchedulesQueryHandler(IBillingScheduleRepository repository, Interfaces.IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BillingScheduleDto>> Handle(GetAllBillingSchedulesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            if (request.IsActive.HasValue)
            {
                entities = entities.Where(e => e.IsActive == request.IsActive.Value);
            }

            var list = new List<BillingScheduleDto>();
            foreach (var entity in entities)
            {
                var dto = _mapper.Map<BillingScheduleDto>(entity);
                if (!string.IsNullOrWhiteSpace(entity.TemplateIds))
                {
                    dto.TemplateIds = JsonSerializer.Deserialize<List<Guid>>(entity.TemplateIds) ?? new List<Guid>();
                }
                list.Add(dto);
            }
            return list;
        }
    }
}


