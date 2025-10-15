using AutoMapper;
using MediatR;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Queries.GetFormTemplatesByCategory
{
    public class GetFormTemplatesByCategoryQueryHandler : IRequestHandler<GetFormTemplatesByCategoryQuery, IEnumerable<FormTemplateDto>>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IMapper _mapper;

        public GetFormTemplatesByCategoryQueryHandler(IFormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FormTemplateDto>> Handle(GetFormTemplatesByCategoryQuery request, CancellationToken cancellationToken)
        {
            var allTemplates = await _formTemplateRepository.GetAllWithSectionsAndFieldsAsync(cancellationToken);
            var filteredTemplates = allTemplates.Where(ft => ft.Category.Equals(request.Category, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<IEnumerable<FormTemplateDto>>(filteredTemplates);
        }
    }
}
