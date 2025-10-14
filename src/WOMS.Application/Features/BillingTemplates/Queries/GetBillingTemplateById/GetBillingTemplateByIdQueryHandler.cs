using System.Text.Json;
using AutoMapper;
using MediatR;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Application.Features.BillingTemplates.Queries.GetBillingTemplateById;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingTemplates.Queries.GetBillingTemplateById
{
    public class GetBillingTemplateByIdQueryHandler : IRequestHandler<GetBillingTemplateByIdQuery, BillingTemplateDto?>
    {
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetBillingTemplateByIdQueryHandler(
            IBillingTemplateRepository billingTemplateRepository,
            AutoMapper.IMapper mapper)
        {
            _billingTemplateRepository = billingTemplateRepository;
            _mapper = mapper;
        }

        public async Task<BillingTemplateDto?> Handle(GetBillingTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var billingTemplate = await _billingTemplateRepository.GetByIdWithFieldOrderAsync(request.Id, cancellationToken);
            if (billingTemplate == null)
            {
                return null;
            }

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
