using MediatR;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Queries.GetAllFormTemplates
{
    public record GetAllFormTemplatesQuery : IRequest<IEnumerable<FormTemplateDto>>;
}

