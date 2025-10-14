using MediatR;
using System.Text.Json;
using WOMS.Application.Features.BillingSchedules.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingSchedules.Queries.GetBillingScheduleById
{
    public class GetBillingScheduleByIdQueryHandler : IRequestHandler<GetBillingScheduleByIdQuery, BillingScheduleDto>
    {
        private readonly IBillingScheduleRepository _repository;
        private readonly Interfaces.IMapper _mapper;

        public GetBillingScheduleByIdQueryHandler(IBillingScheduleRepository repository, Interfaces.IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BillingScheduleDto> Handle(GetBillingScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException("Billing schedule not found");
            }

            var dto = _mapper.Map<BillingScheduleDto>(entity);
            if (!string.IsNullOrWhiteSpace(entity.TemplateIds))
            {
                dto.TemplateIds = JsonSerializer.Deserialize<List<Guid>>(entity.TemplateIds) ?? new List<Guid>();
            }
            return dto;
        }
    }
}


