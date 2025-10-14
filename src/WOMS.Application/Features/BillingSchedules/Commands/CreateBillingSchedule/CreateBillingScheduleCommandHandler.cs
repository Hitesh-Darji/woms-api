using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WOMS.Application.Features.BillingSchedules.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingSchedules.Commands.CreateBillingSchedule
{
    public class CreateBillingScheduleCommandHandler : IRequestHandler<CreateBillingScheduleCommand, BillingScheduleDto>
    {
        private readonly IBillingScheduleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Interfaces.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBillingScheduleCommandHandler(
            IBillingScheduleRepository repository,
            IUnitOfWork unitOfWork,
            Interfaces.IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BillingScheduleDto> Handle(CreateBillingScheduleCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Guid? userIdParsed = Guid.TryParse(userIdClaim, out var parsed) ? parsed : null;

            var entity = _mapper.Map<BillingSchedule>(request.Dto);
            entity.CreatedBy = userIdParsed;
            entity.CreatedOn = DateTime.UtcNow;

            // Ensure TemplateIds JSON is normalized
            if (request.Dto.TemplateIds != null && request.Dto.TemplateIds.Count > 0)
            {
                entity.TemplateIds = JsonSerializer.Serialize(request.Dto.TemplateIds);
            }

            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<BillingScheduleDto>(entity);
            if (!string.IsNullOrWhiteSpace(entity.TemplateIds))
            {
                dto.TemplateIds = JsonSerializer.Deserialize<List<Guid>>(entity.TemplateIds) ?? new List<Guid>();
            }

            return dto;
        }
    }
}


