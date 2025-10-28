using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.Integrations.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Repositories;
using System.Text.Json;

namespace WOMS.Application.Features.Integrations.Commands.UpdateIntegration
{
    public class UpdateIntegrationCommandHandler : IRequestHandler<UpdateIntegrationCommand, IntegrationDto>
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateIntegrationCommandHandler(
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

        public async Task<IntegrationDto> Handle(UpdateIntegrationCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var integration = await _integrationRepository.GetByIdAsync(request.Id, cancellationToken);
            if (integration == null)
            {
                throw new KeyNotFoundException($"Integration with ID {request.Id} not found.");
            }

            // Update properties if provided
            if (!string.IsNullOrEmpty(request.Dto.Name))
                integration.Name = request.Dto.Name;

            if (request.Dto.Category.HasValue)
                integration.Category = request.Dto.Category.Value;

            if (request.Dto.Status.HasValue)
                integration.Status = request.Dto.Status.Value;

            if (request.Dto.Description != null)
                integration.Description = request.Dto.Description;

            if (!string.IsNullOrEmpty(request.Dto.IconName))
                integration.IconName = request.Dto.IconName;

            if (request.Dto.Features != null)
            {
                integration.Features = request.Dto.Features.Any() 
                    ? JsonSerializer.Serialize(request.Dto.Features) 
                    : null;
            }

            if (request.Dto.Configuration != null)
                integration.Configuration = request.Dto.Configuration;

            if (request.Dto.IsActive.HasValue)
                integration.IsActive = request.Dto.IsActive.Value;

            integration.UpdatedBy = userId;
            integration.UpdatedOn = DateTime.UtcNow;

            await _integrationRepository.UpdateAsync(integration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<IntegrationDto>(integration);
        }
    }
}

