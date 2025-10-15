using MediatR;
using WOMS.Application.Features.Forms.DTOs;

namespace WOMS.Application.Features.Forms.Queries.GetActiveFormTemplates
{
    public record GetActiveFormTemplatesQuery : IRequest<IEnumerable<FormTemplateDto>>
    {
    }
}
