using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingRates.Commands.UpdateBillingRate
{
    public class UpdateBillingRateCommandHandler : IRequestHandler<UpdateBillingRateCommand, BillingRateDto>
    {
        private readonly IRateTableRepository _rateTableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateBillingRateCommandHandler(
            IRateTableRepository rateTableRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _rateTableRepository = rateTableRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BillingRateDto> Handle(UpdateBillingRateCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var rateTable = await _rateTableRepository.GetByIdAsync(request.Id, cancellationToken);
            if (rateTable == null)
            {
                throw new KeyNotFoundException($"Rate table with ID '{request.Id}' not found.");
            }

            // Check if rate table with same name already exists (excluding current record)
            if (await _rateTableRepository.ExistsByNameExcludingIdAsync(request.Dto.Name, request.Id, cancellationToken))
            {
                throw new InvalidOperationException($"Rate table with name '{request.Dto.Name}' already exists.");
            }

            // Validate date range
            if (request.Dto.EffectiveStartDate >= request.Dto.EffectiveEndDate)
            {
                throw new InvalidOperationException("Effective start date must be before effective end date.");
            }

            // Update entity manually (following BillingTemplate pattern)
            rateTable.Name = request.Dto.Name;
            rateTable.Description = request.Dto.Description;
            rateTable.RateType = request.Dto.RateType;
            rateTable.BaseRate = request.Dto.BaseRate;
            rateTable.EffectiveStartDate = request.Dto.EffectiveStartDate;
            rateTable.EffectiveEndDate = request.Dto.EffectiveEndDate;
            rateTable.IsActive = request.Dto.IsActive;
            rateTable.UpdatedBy = userId;
            rateTable.UpdatedOn = DateTime.UtcNow;

            await _rateTableRepository.UpdateAsync(rateTable, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BillingRateDto>(rateTable);
        }
    }
}
