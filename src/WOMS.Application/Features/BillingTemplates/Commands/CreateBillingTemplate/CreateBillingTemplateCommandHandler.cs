using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WOMS.Application.Features.BillingTemplates.Commands.CreateBillingTemplate;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Application.Interfaces;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingTemplates.Commands.CreateBillingTemplate
{
    public class CreateBillingTemplateCommandHandler : IRequestHandler<CreateBillingTemplateCommand, BillingTemplateDto>
    {
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBillingTemplateCommandHandler(
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

        public async Task<BillingTemplateDto> Handle(CreateBillingTemplateCommand request, CancellationToken cancellationToken)
        {
            // Get the current user ID from the JWT token
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token or invalid format");
            }

            // Check if billing template with same name already exists for this customer
            if (await _billingTemplateRepository.ExistsByNameForCustomerAsync(request.Name, request.CustomerId, cancellationToken))
            {
                throw new InvalidOperationException($"Billing template with name '{request.Name}' already exists for customer '{request.CustomerName}'.");
            }

            // Serialize field order to JSON
            var fieldOrderJson = JsonSerializer.Serialize(request.FieldOrder);

            var billingTemplate = new BillingTemplate
            {
                Name = request.Name,
                CustomerId = request.CustomerId,
                CustomerName = request.CustomerName,
                OutputFormat = request.OutputFormat,
                FileNamingConvention = request.FileNamingConvention,
                DeliveryMethod = request.DeliveryMethod,
                InvoiceType = request.InvoiceType,
                FieldOrder = fieldOrderJson,
                IsActive = true,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            await _billingTemplateRepository.AddAsync(billingTemplate, cancellationToken);
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
