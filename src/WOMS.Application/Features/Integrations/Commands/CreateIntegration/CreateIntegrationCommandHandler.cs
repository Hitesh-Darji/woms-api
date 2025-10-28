using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Integrations.Commands.CreateIntegration
{
    public class CreateIntegrationCommandHandler : IRequestHandler<CreateIntegrationCommand, IntegrationDto>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateIntegrationCommandHandler(
            IIntegrationRepository integrationRepository,
            IUnitOfWork unitOfWork,
            AutoMapper.IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _integrationRepository = integrationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IntegrationDto> Handle(CreateIntegrationCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            if (await _integrationRepository.ExistsByNameAsync(request.Dto.Name, cancellationToken))
            {
                throw new InvalidOperationException($"Integration with name '{request.Dto.Name}' already exists.");
            }

            string? featuresJson = null;
            if (request.Dto.Features != null && request.Dto.Features.Count != 0)
            {
                featuresJson = JsonSerializer.Serialize(request.Dto.Features);
            }

            var integration = new Integration
            {
                Name = request.Dto.Name,
                Category = request.Dto.Category,
                Status = request.Dto.Status,
                Description = request.Dto.Description,
                IconName = request.Dto.IconName,
                Features = featuresJson,
                Configuration = request.Dto.Configuration,
                IsActive = true,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _integrationRepository.AddAsync(integration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<IntegrationDto>(integration);
        }
    }
}

