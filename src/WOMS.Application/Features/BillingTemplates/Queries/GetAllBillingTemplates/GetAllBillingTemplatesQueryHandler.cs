using System.Text.Json;
using AutoMapper;
using MediatR;
using WOMS.Application.Features.BillingTemplates.DTOs;
using WOMS.Application.Features.BillingTemplates.Queries.GetAllBillingTemplates;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.BillingTemplates.Queries.GetAllBillingTemplates
{
    public class GetAllBillingTemplatesQueryHandler : IRequestHandler<GetAllBillingTemplatesQuery, IEnumerable<BillingTemplateDto>>
    {
        private readonly IBillingTemplateRepository _billingTemplateRepository;
        private readonly AutoMapper.IMapper _mapper;

        public GetAllBillingTemplatesQueryHandler(
            IBillingTemplateRepository billingTemplateRepository,
            AutoMapper.IMapper mapper)
        {
            _billingTemplateRepository = billingTemplateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BillingTemplateDto>> Handle(GetAllBillingTemplatesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<BillingTemplate> billingTemplates;

            if (!string.IsNullOrEmpty(request.CustomerId))
            {
                billingTemplates = await _billingTemplateRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
            }
            else
            {
                billingTemplates = await _billingTemplateRepository.FindAsync(bt => !bt.IsDeleted, cancellationToken);
            }

            var billingTemplateDtos = _mapper.Map<IEnumerable<BillingTemplateDto>>(billingTemplates);
            
            // Deserialize field order for each template
            foreach (var dto in billingTemplateDtos)
            {
                var template = billingTemplates.FirstOrDefault(bt => bt.Id == dto.Id);
                if (template != null && !string.IsNullOrEmpty(template.FieldOrder))
                {
                    dto.FieldOrder = JsonSerializer.Deserialize<List<BillingTemplateFieldDto>>(template.FieldOrder) ?? new List<BillingTemplateFieldDto>();
                }
            }

            return billingTemplateDtos;
        }
    }
}
