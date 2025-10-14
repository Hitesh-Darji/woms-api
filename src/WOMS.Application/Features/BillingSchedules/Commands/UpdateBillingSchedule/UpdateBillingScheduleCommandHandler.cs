using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WOMS.Application.Features.BillingSchedules.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingSchedules.Commands.UpdateBillingSchedule
{
    public class UpdateBillingScheduleCommandHandler : IRequestHandler<UpdateBillingScheduleCommand, BillingScheduleDto>
    {
        private readonly IBillingScheduleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Interfaces.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateBillingScheduleCommandHandler(
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

        public async Task<BillingScheduleDto> Handle(UpdateBillingScheduleCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existing == null)
            {
                throw new KeyNotFoundException("Billing schedule not found");
            }

            // Manually update fields to avoid AutoMapper projecting JSON logic into IQueryable
            existing.Name = request.Dto.Name;
            existing.Frequency = request.Dto.Frequency;
            existing.DayOfWeek = request.Dto.DayOfWeek;
            existing.DayOfMonth = request.Dto.DayOfMonth;
            existing.Time = request.Dto.Time;
            existing.IsActive = request.Dto.IsActive;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            existing.UpdatedBy = Guid.TryParse(userIdClaim, out var parsed) ? parsed : existing.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            if (request.Dto.TemplateIds != null)
            {
                existing.TemplateIds = request.Dto.TemplateIds.Count > 0
                    ? JsonSerializer.Serialize(request.Dto.TemplateIds)
                    : null;
            }

            await _repository.UpdateAsync(existing, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<BillingScheduleDto>(existing);
            if (!string.IsNullOrWhiteSpace(existing.TemplateIds))
            {
                dto.TemplateIds = JsonSerializer.Deserialize<List<Guid>>(existing.TemplateIds) ?? new List<Guid>();
            }
            return dto;
        }
    }
}


