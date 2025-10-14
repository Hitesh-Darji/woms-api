using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.BillingRates.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingRates.Commands.CreateBillingRate
{
    public class CreateBillingRateCommandHandler : IRequestHandler<CreateBillingRateCommand, BillingRateDto>
    {
        private readonly IRateTableRepository _rateTableRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBillingRateCommandHandler(
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

        public async Task<BillingRateDto> Handle(CreateBillingRateCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            // Check if rate table with same name already exists
            if (await _rateTableRepository.ExistsByNameAsync(request.Dto.Name, cancellationToken))
            {
                throw new InvalidOperationException($"Rate table with name '{request.Dto.Name}' already exists.");
            }

            // Validate date range
            if (request.Dto.EffectiveStartDate >= request.Dto.EffectiveEndDate)
            {
                throw new InvalidOperationException("Effective start date must be before effective end date.");
            }

            // Create entity manually (following BillingTemplate pattern)
            var rateTable = new RateTable
            {
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                RateType = request.Dto.RateType,
                BaseRate = request.Dto.BaseRate,
                EffectiveStartDate = request.Dto.EffectiveStartDate,
                EffectiveEndDate = request.Dto.EffectiveEndDate,
                IsActive = request.Dto.IsActive,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _rateTableRepository.AddAsync(rateTable, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BillingRateDto>(rateTable);
        }
    }
}
