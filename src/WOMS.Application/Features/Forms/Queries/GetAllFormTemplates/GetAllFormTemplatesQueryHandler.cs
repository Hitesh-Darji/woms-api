using AutoMapper;
using MediatR;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Queries.GetAllFormTemplates
{
    public class GetAllFormTemplatesQueryHandler : IRequestHandler<GetAllFormTemplatesQuery, IEnumerable<FormTemplateDto>>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IMapper _mapper;

        public GetAllFormTemplatesQueryHandler(IFormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FormTemplateDto>> Handle(GetAllFormTemplatesQuery request, CancellationToken cancellationToken)
        {
            var formTemplates = await _formTemplateRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<FormTemplateDto>>(formTemplates);
        }
    }
}

