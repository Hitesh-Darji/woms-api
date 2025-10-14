using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingTemplates.Commands.UpdateBillingTemplate
{
    public class UpdateBillingTemplateCommandHandler : IRequestHandler<UpdateBillingTemplateCommand, BillingTemplateDto>
    {
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateBillingTemplateCommandHandler(
            IBillingTemplateRepository billingTemplateRepository,
            AutoMapper.IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _billingTemplateRepository = billingTemplateRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BillingTemplateDto> Handle(UpdateBillingTemplateCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            var billingTemplate = await _billingTemplateRepository.GetByIdAsync(request.Id, cancellationToken);
            if (billingTemplate == null)
            {
                throw new InvalidOperationException($"Billing template with ID '{request.Id}' not found.");
            }

            // Check if another billing template with same name already exists for this customer (excluding current one)
            var existingTemplate = await _billingTemplateRepository.GetFirstOrDefaultAsync(
                bt => bt.Name == request.Name && bt.CustomerId == request.CustomerId && bt.Id != request.Id && !bt.IsDeleted, 
                cancellationToken);
            
            if (existingTemplate != null)
            {
                throw new InvalidOperationException($"Billing template with name '{request.Name}' already exists for customer '{request.CustomerName}'.");
            }

            // Serialize field order to JSON
            var fieldOrderJson = JsonSerializer.Serialize(request.FieldOrder);

            // Update properties
            billingTemplate.Name = request.Name;
            billingTemplate.CustomerId = request.CustomerId;
            billingTemplate.CustomerName = request.CustomerName;
            billingTemplate.OutputFormat = request.OutputFormat;
            billingTemplate.FileNamingConvention = request.FileNamingConvention;
            billingTemplate.DeliveryMethod = request.DeliveryMethod;
            billingTemplate.InvoiceType = request.InvoiceType;
            billingTemplate.FieldOrder = fieldOrderJson;
            billingTemplate.IsActive = request.IsActive;
            billingTemplate.UpdatedBy = userId;
            billingTemplate.UpdatedOn = DateTime.UtcNow;

            await _billingTemplateRepository.UpdateAsync(billingTemplate, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var billingTemplateDto = _mapper.Map<BillingTemplateDto>(billingTemplate);
            
            // Deserialize field order for the response
            if (!string.IsNullOrEmpty(billingTemplate.FieldOrder))
            {
                billingTemplateDto.FieldOrder = JsonSerializer.Deserialize<List<BillingTemplateFieldDto>>(billingTemplate.FieldOrder) ?? new List<BillingTemplateFieldDto>();
            }

            return billingTemplateDto;
        }
    }
}
