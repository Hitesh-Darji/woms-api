using AutoMapper;
using MediatR;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Queries.GetActiveFormTemplates
{
    public class GetActiveFormTemplatesQueryHandler : IRequestHandler<GetActiveFormTemplatesQuery, IEnumerable<FormTemplateDto>>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IMapper _mapper;

        public GetActiveFormTemplatesQueryHandler(IFormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FormTemplateDto>> Handle(GetActiveFormTemplatesQuery request, CancellationToken cancellationToken)
        {
            var allTemplates = await _formTemplateRepository.GetAllWithSectionsAndFieldsAsync(cancellationToken);
            var activeTemplates = allTemplates.Where(ft => ft.IsActive);
            return _mapper.Map<IEnumerable<FormTemplateDto>>(activeTemplates);
        }
    }
}
