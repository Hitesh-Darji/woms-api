using AutoMapper;
using MediatR;
using WOMS.Application.Features.Forms.DTOs;
using WOMS.Domain.Repositories;

namespace WOMS.Application.Features.Forms.Queries.GetFormTemplateById
{
    public class GetFormTemplateByIdQueryHandler : IRequestHandler<GetFormTemplateByIdQuery, FormTemplateDto?>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IMapper _mapper;

        public GetFormTemplateByIdQueryHandler(IFormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;
        }

        public async Task<FormTemplateDto?> Handle(GetFormTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var formTemplate = await _formTemplateRepository.GetByIdWithSectionsAndFieldsAsync(request.Id, cancellationToken);
            return formTemplate != null ? _mapper.Map<FormTemplateDto>(formTemplate) : null;
        }
    }
}

